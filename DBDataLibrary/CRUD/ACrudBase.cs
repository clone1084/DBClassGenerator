using DBDataLibrary.Attributes;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace DBDataLibrary.CRUD
{
    public abstract class ACrudBase<TClass> : ICrudClass<TClass> 
        where TClass : ACrudBase<TClass>, new()
    {
        protected const string DT_INSERT_COLUMN = "DT_INSERT";
        protected const string OID_COLUMN = "OID";
        protected const string DT_UPDATE_COLUMN = "DT_UPDATE";
        
        protected HashSet<string> _modifiedProperties = new();

        protected static IList<TClass> cachedEntities = new List<TClass>();

        protected static readonly ReaderWriterLockSlim _cacheLock = new();

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

        public IEnumerable<string> GetKeys()
        {
            var keys = new List<string>();
            var keyProps = GetKeyProperties(); // Reuse the existing method

            foreach (var prop in keyProps)
            {
                // Get the column name, falling back to the property name if no attribute is present
                string columnName = GetColumnName(prop);
                keys.Add(columnName);
            }

            return keys;
        }

        protected static List<PropertyInfo> GetKeyProperties()
        {
            return typeof(TClass).GetProperties()
                .Where(p => p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null)
                .ToList();
        }

        protected List<PropertyInfo> GetAllColumnsProperties()
        {
            return typeof(TClass).GetProperties()
                .Where(p => p.GetCustomAttribute<ColumnNameAttribute>() != null)
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
            var allProps = GetAllColumnsProperties();

            // OID_COLUMN is not insertable, because its auto populated, so we exclude it from the insertable properties
            // DT_INSERT is inserted as SYSDATE by default, so we exclude it from the insertable properties
            var insertableProps = allProps.Except(keyProps)
                .Where(p => !GetColumnName(p).Equals(OID_COLUMN, StringComparison.OrdinalIgnoreCase)
                         && !GetColumnName(p).Equals(DT_INSERT_COLUMN, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var dtInsertProp = allProps.FirstOrDefault(p => GetColumnName(p).Equals(DT_INSERT_COLUMN, StringComparison.OrdinalIgnoreCase));

            var columnListItems = insertableProps.Select(GetColumnName).ToList();
            var paramListItems = insertableProps.Select(p => ":" + GetColumnName(p)).ToList();

            if (dtInsertProp != null)
            {
                columnListItems.Add(DT_INSERT_COLUMN);
                paramListItems.Add("SYSDATE");
            }

            var columnList = string.Join(", ", columnListItems);
            var paramList = string.Join(", ", paramListItems);

            var outProps = keyProps;
            if (dtInsertProp != null)
            {
                outProps.Add(dtInsertProp);
            }
            var returningClause = outProps.Any()
                ? $" RETURNING {string.Join(", ", outProps.Select(GetColumnName))} INTO {string.Join(", ", outProps.Select(p => ":" + GetColumnName(p) + "_OUT"))}"
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

            foreach (var outProp in outProps)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + GetColumnName(outProp) + "_OUT";
                param.Direction = ParameterDirection.Output;

                // Mapping tipo C# → tipo Db
                param.DbType = MapTypeToDbType(outProp.PropertyType);
                cmd.Parameters.Add(param);

                outputParams[outProp] = param;
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
                return true;

            var type = typeof(TClass);
            var propsModified = modified
                .Select(name => type.GetProperty(name))
                .Where(p => p != null && p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() == null)
                .ToList();

            if (!propsModified.Any())
                return true;

            var keyProps = GetKeyProperties();
            if (!keyProps.Any())
                throw new InvalidOperationException("No [Key] properties found for update.");

            var dtUpdateProp = type.GetProperties().FirstOrDefault(p => GetColumnName(p).Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase));

            var setAssignments = new List<string>();
            var outputParams = new Dictionary<PropertyInfo, IDataParameter>();
            
            foreach (var prop in propsModified)
            {
                var column = GetColumnName(prop);

                if (column.Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Skip DT_UPDATE column in set assignments
                }
                else
                {
                    setAssignments.Add($"{column} = :{column}");
                }
            }

            if (dtUpdateProp != null && setAssignments.Any())
            {
                setAssignments.Add($"{DT_UPDATE_COLUMN} = SYSDATE");
            }

            var setClause = string.Join(", ", setAssignments);
            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));

            // Prepare RETURNING clause if DT_UPDATE is in the update
            string returningClause = string.Empty;
            if (dtUpdateProp != null)
            {
                returningClause = $" RETURNING {DT_UPDATE_COLUMN} INTO :{DT_UPDATE_COLUMN}_OUT";
            }

            var sql = $"UPDATE {TableName} SET {setClause} WHERE {whereClause}{returningClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Input parameters
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

            // Output parameter for DT_UPDATE
            if (dtUpdateProp != null)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = ":" + DT_UPDATE_COLUMN + "_OUT";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.DateTime;
                cmd.Parameters.Add(param);
                outputParams[dtUpdateProp] = param;
            }

            var affected = cmd.ExecuteNonQuery();

            // Set output values
            foreach (var kv in outputParams)
            {
                var prop = kv.Key;
                var param = kv.Value;

                if (param.Value != DBNull.Value)
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    prop.SetValue(this, Convert.ChangeType(param.Value, targetType));
                }
            }

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


        public static TClass Load(IDbConnection connection, params KeyValuePair<string, object>[] keyValues)
        {
            if (HasTableTypeFlag(TableTypes.Cached))
            {
                _cacheLock.EnterReadLock();
                try
                {
                    var cachedItem = cachedEntities.ToList().FirstOrDefault(e => e.GetKeyValues().SequenceEqual(keyValues));
                    if (cachedItem != null)
                    {
                        return cachedItem;
                    }
                }
                finally
                {
                    _cacheLock.ExitReadLock();
                }
            }

            var keyProps = GetKeyProperties();
            if (keyProps.Count != keyValues.Length)
                throw new ArgumentException("Number of key values does not match number of [Key] properties.");

            var whereClause = string.Join(" AND ", keyProps.Select(p => $"{GetColumnName(p)} = :{GetColumnName(p)}"));
            var sql = $"SELECT * FROM {GetTableName()} WHERE {whereClause}";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            var kvpDict = keyValues.ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var keyProp in keyProps)
            {
                var colName = GetColumnName(keyProp);
                if (!kvpDict.Keys.Contains(colName, StringComparer.OrdinalIgnoreCase))
                    throw new ArgumentException($"Key value for '{colName}' is missing in the provided key values.");

                var param = cmd.CreateParameter();
                param.ParameterName = ":" + colName;
                param.Value = kvpDict[colName];
                cmd.Parameters.Add(param);
            }

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return default!;

            var loadedInstance = ReadData(reader);

            if (HasTableTypeFlag(TableTypes.Cached))
            {
                _cacheLock.EnterWriteLock();
                try
                {
                    if (!cachedEntities.Contains(loadedInstance))
                    {
                        cachedEntities.Add(loadedInstance);
                    }
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }

            return loadedInstance;
        }


        private static TClass ReadData(IDataReader reader)
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
            // Clear the modified properties since we just loaded the data
            instance._modifiedProperties.Clear();
            return instance;
        }

        /// <summary>
        /// This method retrieves all records of type TClass from the database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="whereFilter">In case of cached table, this filter will be ignored and you need to perform a <see cref="Where()"/></param> on the resuls of the method.
        /// <param name="reloadCache">In case of cached table, this will flush the cached data e load fresh data from DB</param>
        /// <returns></returns>
        public static IEnumerable<TClass> LoadAll(IDbConnection connection, string whereFilter = "", bool reloadCache = false)
        {
            if (HasTableTypeFlag(TableTypes.Cached))
            {
                _cacheLock.EnterUpgradeableReadLock();
                try
                {
                    if (reloadCache)
                    {
                        _cacheLock.EnterWriteLock();
                        try
                        {
                            cachedEntities.Clear();
                        }
                        finally
                        {
                            _cacheLock.ExitWriteLock();
                        }
                    }

                    if (cachedEntities.Any())
                    {
                        return cachedEntities.ToList();
                    }
                }
                finally
                {
                    _cacheLock.ExitUpgradeableReadLock();
                }
                // if cache is empty or cleared, proceed to reload from DB
                whereFilter = string.Empty;
            }

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
                var loadedInstance = ReadData(reader);
                resultList.Add(loadedInstance);
            }

            if (HasTableTypeFlag(TableTypes.Cached))
            {
                _cacheLock.EnterWriteLock();
                try
                {
                    cachedEntities = resultList;
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }

            return resultList;
        }

    }
}
