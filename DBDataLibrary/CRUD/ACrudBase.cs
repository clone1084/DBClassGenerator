using DBDataLibrary.Attributes;
using DBDataLibrary.DataTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.CRUD
{
    public abstract class ACrudBase<TClass, TData>
        where TClass : ACrudBase<TClass, TData>, new()
        where TData : IDBData, new()
    {
        protected TData Data;
        protected HashSet<string> _modifiedProperties = new();

        protected ACrudBase()
        {
            Data = new TData();
            //Data.SetCrudClass(this);
        }

        /// <summary>
        /// Use <see cref="GetData"/> to modify the data.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        protected void Set<TValue>(string propertyName, TValue value)
        {
            var prop = typeof(TData).GetProperty(propertyName);
            if (prop != null)
            {
                prop.SetValue(Data, value);
                _modifiedProperties.Add(propertyName);
            }
            else
            {
                throw new ArgumentException($"Property '{propertyName}' not found on {typeof(TData).Name}");
            }
        }

        /// <summary>
        /// Use <see cref="GetData"/> to get the data.
        /// </summary>
        protected TValue Get<TValue>(string propertyName)
        {
            var prop = typeof(TData).GetProperty(propertyName);
            var value = prop?.GetValue(Data);

            // Controllo se TValue è nullable
            var tValueType = typeof(TValue);
            bool isNullable = !tValueType.IsValueType || Nullable.GetUnderlyingType(tValueType) != null;

            if (value is null)
            {
                if (isNullable)
                {
                    return default!;
                }
                else
                {
                    throw new InvalidOperationException($"Il valore della proprietà '{propertyName}' è null, ma il tipo '{typeof(TValue).Name}' non ammette null.");
                }
            }

            return (TValue)value;
        }

        public TData GetData() => Data;

        public abstract string TableName { get; }

        /// <summary>
        /// Retrieves a dictionary containing the names and values of the properties marked with the [Key] attribute.
        /// </summary>
        /// <returns>A dictionary where the key is the property name (or its column name if specified) and the value is the property's value.</returns>
        public Dictionary<string, object> GetKeyValues()
        {
            var keyValues = new Dictionary<string, object>();
            var keyProps = GetKeyProperties(); // Reuse the existing method

            foreach (var prop in keyProps)
            {
                // Get the column name, falling back to the property name if no attribute is present
                string columnName = GetColumnName(prop);

                // Get the value of the key property from the Data instance
                object? value = prop.GetValue(Data);

                keyValues[columnName] = value ?? DBNull.Value;
            }

            return keyValues;
        }

        protected List<PropertyInfo> GetKeyProperties()
        {
            return typeof(TData).GetProperties()
                .Where(p => p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null)
                .ToList();
        }

        protected string GetColumnName(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<ColumnNameAttribute>();
            return attr != null ? attr.Name : prop.Name;
        }

        public bool Insert(IDbConnection connection)
        {
            var keyProps = GetKeyProperties();
            var insertableProps = GetKeyProperties();

            var columnList = string.Join(", ", insertableProps.Select(GetColumnName));
            var paramList = string.Join(", ", insertableProps.Select(p => ":" + GetColumnName(p)));

            var returningClause = keyProps.Any()
                ? $" RETURNING {string.Join(", ", keyProps.Select(GetColumnName))} INTO {string.Join(", ", keyProps.Select(p => ":" + GetColumnName(p) + "_OUT"))}"
                : string.Empty;

            var sql = $@"
                INSERT INTO {TableName} ({columnList})
                VALUES ({paramList}){returningClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Parametri di input
            foreach (var prop in insertableProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(prop);
                param.Value = prop.GetValue(Data) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            // Parametri di output per le chiavi
            var outputParams = new Dictionary<PropertyInfo, IDataParameter>();

            foreach (var keyProp in keyProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(keyProp) + "_OUT";
                param.Direction = ParameterDirection.Output;

                // Mapping tipo C# → tipo Db
                param.DbType = MapTypeToDbType(keyProp.PropertyType);
                cmd.Parameters.Add(param);

                outputParams[keyProp] = param;
            }

            cmd.ExecuteNonQuery();

            // Recupero dei valori autogenerati
            foreach (var kv in outputParams)
            {
                var prop = kv.Key;
                var param = kv.Value;

                if (param.Value != DBNull.Value)
                    prop.SetValue(Data, Convert.ChangeType(param.Value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
            }

            _modifiedProperties.Clear();
            return true;
        }

        private DbType MapTypeToDbType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return Type.GetTypeCode(type) switch
            {
                TypeCode.Decimal => DbType.Decimal,
                TypeCode.Int32 => DbType.Int32,
                TypeCode.Int64 => DbType.Int64,
                TypeCode.String => DbType.String,
                TypeCode.DateTime => DbType.DateTime,
                TypeCode.Double => DbType.Double,
                TypeCode.Boolean => DbType.Boolean,
                _ => DbType.Object
            };
        }

        public bool Update(IDbConnection connection)
        {
            var modified = _modifiedProperties.ToList();

            if (!modified.Any())
                return false;

            var type = typeof(TData);
            var propsModified = modified
                .Select(name => type.GetProperty(name))
                .Where(p => p != null && p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() == null)
                .ToList();

            if (!propsModified.Any())
                return false;

            var keyProps = GetKeyProperties();
            if (!keyProps.Any())
                throw new InvalidOperationException("No [Key] properties found for update.");

            var setClause = string.Join(", ", propsModified.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));
            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));

            var sql = $"UPDATE {TableName} SET {setClause} WHERE {whereClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            foreach (var prop in propsModified)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(prop);
                param.Value = prop.GetValue(Data) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            foreach (var keyProp in keyProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(keyProp);
                param.Value = keyProp.GetValue(Data) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            var affected = cmd.ExecuteNonQuery();
            if (affected > 0)
                _modifiedProperties.Clear();

            return affected > 0;
        }

        public bool Delete(IDbConnection connection)
        {
            var keyProps = GetKeyProperties();
            if (!keyProps.Any())
                throw new InvalidOperationException("No [Key] properties found for delete.");

            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));
            var sql = $"DELETE FROM {TableName} WHERE {whereClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            foreach (var keyProp in keyProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(keyProp);
                param.Value = keyProp.GetValue(Data) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            return cmd.ExecuteNonQuery() > 0;
        }

        public TData Load(IDbConnection connection, params object[] keyValues)
        {
            var keyProps = GetKeyProperties();
            if (keyProps.Count != keyValues.Length)
                throw new ArgumentException("Number of key values does not match number of [Key] properties.");

            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));
            var sql = $"SELECT * FROM {TableName} WHERE {whereClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            for (int i = 0; i < keyProps.Count; i++)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(keyProps[i]);
                param.Value = keyValues[i];
                cmd.Parameters.Add(param);
            }

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return default!;
            
            var dataInstance = ReadData(reader);
            Data = dataInstance; 
            _modifiedProperties.Clear();
            return dataInstance;
        }

        private TData ReadData(IDataReader reader)
        {
            var instance = new TData();
            var props = typeof(TData).GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                if (!props.TryGetValue(name, out var prop) || !prop.CanWrite)
                    continue;

                var value = reader.IsDBNull(i) ? null : reader.GetValue(i); 
                
                if (value != null)
                {
                    // Get the underlying type of the property (e.g., Int32 for Nullable<Int32>)
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    // Convert the value from the reader to the target type of the property
                    value = Convert.ChangeType(value, targetType);
                }

                prop.SetValue(instance, value);
            }            
            return instance;
        }

        public IEnumerable<TClass> LoadAll(IDbConnection connection, string whereFilter = "")
        {
            var sql = $"SELECT * FROM {TableName}";
            if (!string.IsNullOrWhiteSpace(whereFilter))
            {
                sql += " WHERE " + whereFilter;
            }

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            using var reader = cmd.ExecuteReader();
            var resultList = new List<TClass>();
            var type = typeof(TData);
            var props = type.GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

            while (reader.Read())
            {
                var instance = (TClass)Activator.CreateInstance(typeof(TClass), true);
                // Fix: Verifica che instance non sia null prima di chiamare ReadData
                if (instance != null)
                {
                    instance.ReadData(reader);
                    resultList.Add(instance);
                }
            }

            return resultList;
        }
    }
}
