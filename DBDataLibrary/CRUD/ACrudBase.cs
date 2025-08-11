using DBDataLibrary.Attributes;
using DBDataLibrary.Utils;
using DBDataLibrary.Utils;
using log4net;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DBDataLibrary.CRUD
{
    public abstract class ACrudBase<TClass> : ICrudClass<TClass> , IEquatable<TClass>
        where TClass : ACrudBase<TClass>, new()
    {
        protected const string OID_COLUMN = "OID";
        protected const string DT_INSERT_COLUMN = "DT_INSERT";
        protected const string DT_UPDATE_COLUMN = "DT_UPDATE";
        
        protected HashSet<string> _modifiedProperties = new();

        protected static readonly ConcurrentDictionary<string, TClass> _cache = new();
        protected static readonly ConcurrentDictionary<string, ConcurrentDictionary<object, TClass>> _indexes = new();


        protected ACrudBase() { }

        protected static void AddToCache(TClass instance)
        {
            var compositeKey = instance.GetInstanceCompositeKey();
            _cache[compositeKey] = instance;

            foreach (var keyProp in GetKeyProperties())
            {
                var indexName = keyProp.Name;
                var keyValue = keyProp.GetValue(instance);
                if (keyValue == null) continue;

                var index = _indexes.GetOrAdd(indexName, _ => new ConcurrentDictionary<object, TClass>());
                index[keyValue] = instance;
            }
        }

        private static void RemoveFromCache(TClass entity)
        {
            foreach (var keyProp in GetKeyProperties())
            {
                var indexName = keyProp.Name;
                var keyValue = keyProp.GetValue(entity);

                if (keyValue is null) continue;

                if (_indexes.TryGetValue(indexName, out var index))
                {
                    index.TryRemove(keyValue, out _);
                }
            }
        }

        protected static TClass? TryFromIndex(string propertyName, object keyValue)
        {
            if (_indexes.TryGetValue(propertyName, out var index) && index.TryGetValue(keyValue, out var result))
            {
                return result;
            }
            return null;

        }

        protected void AddModifiedProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            }

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

        public IEnumerable<string> GetKeys() => GetKeyProperties().Select(GetColumnName);

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

        protected static string GetCompositeKey(Dictionary<string, object> keyValues) 
        {
            return string.Join("::", keyValues.OrderBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                .Select(kv => $"{kv.Key.ToUpperInvariant()}={kv.Value?.ToString()?.ToUpperInvariant()}"));        
        }

        protected string GetInstanceCompositeKey() => GetCompositeKey(GetKeyValues());

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

        public bool IsCached()
        {
            return HasTableTypeFlag(TableTypes.Cached);
        }

        private static bool HasTableTypeFlag(TableTypes required)
        {
            var attr = typeof(TClass).GetCustomAttribute<TableTypeAttribute>();
            return attr != null && attr.TableType.HasFlag(required);
        }

        public bool Insert(IDbConnection connection, ILog log, string baseLogMessage)
        {
            baseLogMessage += $"{typeof(TClass).Name}.Insert: ";

            if (HasTableTypeFlag(TableTypes.ReadOnly))
            {
                log.Error($"{baseLogMessage} - Insert operation is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
                throw new InvalidOperationException($"Insert is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
            }

            if (!HasTableTypeFlag(TableTypes.Insertable))
            {
                log.Error($"{baseLogMessage} - Insert operation is not allowed for table type '{typeof(TClass).Name}'.");
                throw new InvalidOperationException($"Insert is not allowed for table type '{typeof(TClass).Name}'.");
            }

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
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

                    bool inserted = cmd.ExecuteNonQuery() == 1;

                    // Recupero dei valori autogenerati
                    foreach (var kv in outputParams)
                    {
                        var prop = kv.Key;
                        var param = kv.Value;

                        if (param.Value != DBNull.Value)
                            prop.SetValue(this, Convert.ChangeType(param.Value, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
                    }

                    if (HasTableTypeFlag(TableTypes.Cached) && inserted)
                    {
                        AddToCache((TClass)this);
                    }

                    _modifiedProperties.Clear();

                    return inserted;
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error inserting record: {ex.Message}", ex);
                    etl.CaptureException(ex);
                    return false;
                }
            }
        }

        public bool Update(IDbConnection connection, ILog log, string baseLogMessage)
        {
            baseLogMessage += $"{typeof(TClass).Name}.Update: ";

            if (HasTableTypeFlag(TableTypes.ReadOnly))
            {
                log.Error($"{baseLogMessage} - Update operation is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
                throw new InvalidOperationException($"Update is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
            }

            if (!HasTableTypeFlag(TableTypes.Updatable))
            {
                log.Error($"{baseLogMessage} - Update operation is not allowed for table type '{typeof(TClass).Name}'.");
                throw new InvalidOperationException($"Update is not allowed for table type '{typeof(TClass).Name}'.");
            }

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
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
                    {
                        log.Error($"{baseLogMessage} - No [Key] properties found for update operation in table '{typeof(TClass).Name}'.");
                        throw new InvalidOperationException("No [Key] properties found for update.");
                    }

                    var dtUpdateProp = type.GetProperties().FirstOrDefault(p => GetColumnName(p).Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase));

                    var setAssignments = new List<string>();
                    var outputParams = new Dictionary<PropertyInfo, IDataParameter>();

                    foreach (var prop in propsModified)
                    {
                        if (prop == null)
                            continue;

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
                    foreach (var prop in propsModified.Where(p => p != null && !GetColumnName(p).Equals(DT_UPDATE_COLUMN, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (prop == null)
                            continue;

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

                    bool updated = cmd.ExecuteNonQuery() > 0;

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

                    if (updated)
                    {
                        _modifiedProperties.Clear();
                        if (HasTableTypeFlag(TableTypes.Cached))
                        {
                            AddToCache((TClass)this);
                        }
                    }

                    return updated;
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error updating record: {ex.Message}", ex);
                    etl.CaptureException(ex);
                    return false;
                }
            }
        }

        public bool Delete(IDbConnection connection, ILog log, string baseLogMessage)
        {
            baseLogMessage += $"{typeof(TClass).Name}.Delete: ";

            if (HasTableTypeFlag(TableTypes.ReadOnly))
            {
                log.Error($"{baseLogMessage} - Delete operation is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
                throw new InvalidOperationException($"Delete is not allowed! Table '{typeof(TClass).Name}' is marked as ReadOnly.");
            }

            if (!HasTableTypeFlag(TableTypes.Deletable))
            {
                log.Error($"{baseLogMessage} - Delete operation is not allowed for table type '{typeof(TClass).Name}'.");
                throw new InvalidOperationException($"Delete is not allowed for table type '{typeof(TClass).Name}'.");
            }

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
                    var keyProps = GetKeyProperties();
                    if (!keyProps.Any())
                    {
                        log.Error($"{baseLogMessage} - No [Key] properties found for delete operation in table '{typeof(TClass).Name}'.");
                        throw new InvalidOperationException("No [Key] properties found for delete.");
                    }

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

                    bool result = cmd.ExecuteNonQuery() > 0;

                    if (HasTableTypeFlag(TableTypes.Cached) && result)
                    {
                        RemoveFromCache((TClass)this);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error deleting record: {ex.Message}", ex);
                    etl.CaptureException(ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// This method retrieves a single record of type TClass from the database based on the provided where expression.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="log"></param>
        /// <param name="baseLogMessage"></param>
        /// <param name="whereExpression"></param>
        /// <param name="ignoreCache"></param>
        /// <returns></returns>
        public static TClass Get(IDbConnection connection, ILog log, string baseLogMessage, Expression<Func<TClass, bool>> whereExpression, bool ignoreCache = false)
        {
            baseLogMessage += $"{typeof(TClass).Name}.Get: ";
            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
                    if (HasTableTypeFlag(TableTypes.Cached) && !ignoreCache)
                    {
                        if (CacheKeyExtractor.TryExtractSingleKey(whereExpression, out var keyName, out var keyValue))
                        {
                            var fromIndex = TryFromIndex(keyName, keyValue);
                            if (fromIndex != null)
                                return fromIndex;
                        }
                        else if (CacheKeyExtractor.TryExtractKeyValues(whereExpression, GetKeyProperties(), out var keyDict))
                        {
                            var cacheKey = GetCompositeKey(keyDict);
                            if (_cache.TryGetValue(cacheKey, out var cached))
                                return cached;
                        }
                    }

                    using var cmd = connection.CreateCommand();

                    var builder = new OracleWhereBuilder();
                    var (whereClause, parameters) = builder.BuildWhere(whereExpression);
                    // ✅ Aggiunta sicura dei parametri
                    var sql = $"SELECT * FROM {GetTableName()} WHERE {whereClause}";

                    cmd.CommandText = sql;
                    DbCommandHelper.AddParameters(cmd, parameters);

                    using var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                        return default!;

                    var loadedInstance = ReadData(reader, log, baseLogMessage);
                    if (HasTableTypeFlag(TableTypes.Cached))
                        AddToCache(loadedInstance);

                    return loadedInstance;
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error retrieving record: {ex.Message}", ex);
                    etl.CaptureException(ex);
                    return default!;
                }
            }
        }


        /// <summary>
        /// Gets all records of type TClass from the database. If the table is marked as Cached, it will return the cached version by default.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="log"></param>
        /// <param name="baseLogMessage"></param>
        /// <returns></returns>
        public static IEnumerable<TClass> GetMany(IDbConnection connection, ILog log, string baseLogMessage)
        {
            if (HasTableTypeFlag(TableTypes.Cached))
            {
                // return the cached version by default
                return GetMany(connection, log, baseLogMessage, null, false);
            }
            // return SQL version
            return GetMany(connection, log, baseLogMessage, "");
        }

        /// <summary>
        /// This method retrieves records of type TClass from the database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="log"></param>
        /// <param name="baseLogMessage"></param>
        /// <param name="whereExpression"></param>
        /// <param name="reloadCache">In case of cached table, this will flush the cached data e load fresh data from DB</param>
        /// <param name="ignoreCache">In case of cached table, this will ignore the cache and search on DB</param>
        /// <returns></returns>
        public static IEnumerable<TClass> GetMany(IDbConnection connection, ILog log, string baseLogMessage, Expression<Func<TClass, bool>>? whereExpression = null, bool ignoreCache = false)
        {
            baseLogMessage += $"{typeof(TClass).Name}.GetMany: ";
            var resultList = new List<TClass>();

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
                    // If the table is marked as Cached, we will use the cache if available
                    if (!ignoreCache && HasTableTypeFlag(TableTypes.Cached))
                    {
                        if (!_cache.Any())
                        {
                            LoadCache(connection, log, baseLogMessage);
                        }

                        if (_cache.Any())
                        {
                            var cachedList = _cache.Values;
                            if (whereExpression != null)
                            {
                                var predicate = whereExpression.Compile();
                                return cachedList.Where(predicate).ToList();
                            }
                            return cachedList.ToList();
                        }
                    }

                    // If we are here, it means we are not using cache
                    using var cmd = connection.CreateCommand();
                    var sql = $"SELECT * FROM {GetTableName()} ";
                    if (whereExpression != null)
                    {
                        // ✅ Usa OracleWhereBuilder per creare clausola e parametri
                        var builder = new OracleWhereBuilder();
                        var (whereClause, parameters) = builder.BuildWhere(whereExpression);
                        // ✅ Aggiunta sicura dei parametri
                        DbCommandHelper.AddParameters(cmd, parameters);
                        sql += $" WHERE {whereClause} ";
                    }

                    cmd.CommandText = sql;

                    using var reader = cmd.ExecuteReader();
                    var type = typeof(TClass);
                    var props = type.GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

                    while (reader.Read())
                    {
                        var loadedInstance = ReadData(reader, log, baseLogMessage);
                        resultList.Add(loadedInstance);
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error retrieving records: {ex.Message}", ex);
                    etl.CaptureException(ex);
                }
            }

            return resultList;
        }

        /// <summary>
        /// This method retrieves all records of type TClass from the database WITHOUT USING CACHE.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="log"></param>
        /// <param name="baseLogMessage"></param>
        /// <param name="whereFilter"></param>
        /// <returns></returns>
        public static IEnumerable<TClass> GetMany(IDbConnection connection, ILog log, string baseLogMessage, string whereFilter = "", Dictionary<string, object?>? parameters = null)
        {
            baseLogMessage += $"{typeof(TClass).Name}.GetMany: ";

            var resultList = new List<TClass>();

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
                    var sql = $"SELECT * FROM {GetTableName()}";
                    if (!string.IsNullOrWhiteSpace(whereFilter))
                    {
                        baseLogMessage += $" WHERE {whereFilter}: ";
                        sql += " WHERE " + whereFilter;
                    }

                    using var cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    if (parameters != null)
                    {
                        foreach (var kvp in parameters)
                        {
                            // ✅ Aggiunta sicura dei parametri
                            DbCommandHelper.AddParameters(cmd, parameters);
                        }
                    }

                    using var reader = cmd.ExecuteReader();
                    var type = typeof(TClass);
                    var props = type.GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

                    while (reader.Read())
                    {
                        var loadedInstance = ReadData(reader, log, baseLogMessage);
                        resultList.Add(loadedInstance);
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMessage}Error retrieving records: {ex.Message}", ex);
                    etl.CaptureException(ex);
                } 
            }

            return resultList;
        }

        public void ReLoadCache(IDbConnection connection, ILog log, string baseLogMessage, CancellationToken cancellationToken)
        {
            baseLogMessage += $" {typeof(TClass).Name}.ReLoadCache: ";
            if (cancellationToken.IsCancellationRequested)
            {
                log.Debug($"{baseLogMessage}{TableName}: Cache refresh cancelled.");
                return;
            }
            LoadCache(connection, log, baseLogMessage);
        }

        public static void LoadCache(IDbConnection connection, ILog log, string baseLogMessage)
        {
            baseLogMessage += $" LoadCache for table '{typeof(TClass).Name}': ";
            if (!HasTableTypeFlag(TableTypes.Cached))
            {
                log.Error($"{baseLogMessage} Table '{typeof(TClass).Name}' is not marked as Cached. Cannot load cache.");
                throw new InvalidOperationException($"Table '{typeof(TClass).Name}' is not marked as Cached. Cannot load cache.");
            }

            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug))
            {
                try
                {
                    _cache.Clear();
                    log.Debug($"{baseLogMessage} Cache cleared for table '{typeof(TClass).Name}'.");

                    var sql = $"SELECT * FROM {GetTableName()}";

                    using var cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    using var reader = cmd.ExecuteReader();
                    var resultList = new List<TClass>();
                    var type = typeof(TClass);
                    var props = type.GetProperties().ToDictionary(p => GetColumnName(p), p => p, StringComparer.OrdinalIgnoreCase);

                    while (reader.Read())
                    {
                        var loadedInstance = ReadData(reader, log, baseLogMessage);
                        resultList.Add(loadedInstance);

                        if (HasTableTypeFlag(TableTypes.Cached))
                        {
                            AddToCache(loadedInstance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    etl.CaptureException(ex);
                    log.Error($"{baseLogMessage}Error loading cache for table '{typeof(TClass).Name}': {ex.Message}", ex);
                }
            }
        }

        private static TClass ReadData(IDataReader reader, ILog log, string baseLogMessage)
        {
            baseLogMessage += $"ReadData: ";

            var instance = new TClass();
            using (ExecutionTimerLogger etl = new ExecutionTimerLogger(log, baseLogMessage, LogLevel.Debug, false))
            {
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
            }
            return instance;
        }

        public bool Equals(TClass? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            var props = GetAllColumnsProperties();

            foreach (var prop in props)
            {
                var thisValue = prop.GetValue(this);
                var otherValue = prop.GetValue(other);

                if (thisValue == null && otherValue == null)
                    continue;

                if (thisValue == null || otherValue == null || !thisValue.Equals(otherValue))
                    return false;
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is TClass other && Equals(other);
        }

        public override int GetHashCode()
        {
            var props = GetAllColumnsProperties();
            unchecked
            {
                int hash = 17;
                foreach (var prop in props)
                {
                    var value = prop.GetValue(this);
                    hash = hash * 23 + (value?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }
    }
}
