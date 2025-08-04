using DBDataLibrary.Attributes;
using System.Data;
using System.Reflection;

namespace DBDataLibrary.CRUD
{
    public abstract class ACrudBase<TClass> : ICrudClass<TClass> 
        where TClass : ACrudBase<TClass>, new()
    {
        protected const string DT_UPDATE_COLUMN = "DT_UPDATE";
        protected HashSet<string> _modifiedProperties = new();

        protected ACrudBase() { }

        protected void AddModifiedProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));

            if (!_modifiedProperties.Contains(propertyName))
                _modifiedProperties.Add(propertyName);
        }

        private static string _tableName = "";
        protected static string GetTableName()
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                var attr = typeof(TClass).GetCustomAttribute<TableNameAttribute>();
                _tableName = attr != null ? attr.TableName : typeof(TClass).Name;
            }
            
            return _tableName;
        }

        public string TableName => GetTableName();

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

                // Get the value of the key property from the instance
                object? value = prop.GetValue(this);

                keyValues[columnName] = value ?? DBNull.Value;
            }

            return keyValues;
        }

        protected List<PropertyInfo> GetKeyProperties()
        {
            return typeof(TClass).GetProperties()
                .Where(p => p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null)
                .ToList();
        }

        protected static string GetColumnName(PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<ColumnNameAttribute>();
            return attr != null ? attr.Name : prop.Name;
        }

        public bool Insert(IDbConnection connection)
        {
            if (!HasTableTypeFlag(TableTypes.Insertable))
                throw new InvalidOperationException($"Insert is not allowed for table type '{typeof(TClass).Name}'.");


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
                param.Value = prop.GetValue(this) ?? DBNull.Value;
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
                    prop.SetValue(this, Convert.ChangeType(param.Value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
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

        private static bool HasTableTypeFlag(TableTypes required)
        {
            var attr = typeof(TClass).GetCustomAttribute<TableTypeAttribute>();
            return attr != null && attr.TableType.HasFlag(required);
        }

        public bool Update(IDbConnection connection)
        {
            if (!HasTableTypeFlag(TableTypes.Updatable))
                throw new InvalidOperationException($"Update is not allowed for table type '{typeof(TClass).Name}'.");


            var modified = _modifiedProperties.ToList();

            if (!modified.Any())
                return false;

            var type = typeof(TClass);
            var propsModified = modified
                .Select(name => type.GetProperty(name))
                .Where(p => p != null && p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() == null)
                .ToList();

            if (!propsModified.Any())
                return false;

            var keyProps = GetKeyProperties();
            if (!keyProps.Any())
                throw new InvalidOperationException("No [Key] properties found for update.");

            //var setClause = string.Join(", ", propsModified.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));
            var setAssignments = new List<string>();
            foreach (var prop in propsModified)
            {
                var column = GetColumnName(prop);
                if (column.Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase))
                {
                    setAssignments.Add($"{column} = SYSDATE");
                }
                else
                {
                    setAssignments.Add($"{column} = :{column}");
                }
            }
            var setClause = string.Join(", ", setAssignments);

            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));

            var sql = $"UPDATE {TableName} SET {setClause} WHERE {whereClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            //foreach (var prop in propsModified)
            foreach (var prop in propsModified.Where(p => !GetColumnName(p).Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase)))
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(prop);
                param.Value = prop.GetValue(this) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            foreach (var keyProp in keyProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(keyProp);
                param.Value = keyProp.GetValue(this) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            var affected = cmd.ExecuteNonQuery();
            if (affected > 0)
                _modifiedProperties.Clear();

            return affected > 0;
        }

        public bool Delete(IDbConnection connection)
        {
            if (!HasTableTypeFlag(TableTypes.Deletable))
                throw new InvalidOperationException($"Delete is not allowed for table type '{typeof(TClass).Name}'.");


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
                param.Value = keyProp.GetValue(this) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            return cmd.ExecuteNonQuery() > 0;
        }

        public TClass Load(IDbConnection connection, params object[] keyValues)
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
            _modifiedProperties.Clear();
            return dataInstance;
        }

        private TClass ReadData(IDataReader reader)
        {
            var instance = new TClass();
            var props = typeof(TClass).GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

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

        public static IEnumerable<TClass> LoadAll(IDbConnection connection, string whereFilter = "")
        {
            var sql = $"SELECT * FROM {GetTableName()}";
            if (!string.IsNullOrWhiteSpace(whereFilter))
            {
                sql += " WHERE " + whereFilter;
            }

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            using var reader = cmd.ExecuteReader();
            var resultList = new List<TClass>();
            var type = typeof(TClass);
            var props = type.GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);
            
            
            while (reader.Read())
            {
                var instanceObj = Activator.CreateInstance(typeof(TClass), true);
                if (instanceObj is TClass instance)
                {
                    instance.ReadData(reader);
                    resultList.Add(instance);
                }
            }

            return resultList;
        }
    }
}
