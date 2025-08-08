using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using DBDataLibrary.DbUtils;
using DBDataLibrary.Tables;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTestApp
{
    internal class DBCrudTester
    {
        static string connectionString = "";

        public DBCrudTester(string[] args)
        {
            ParseArgs(args);
        }

        static void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-cs") connectionString = args[i + 1];
                //if (args[i] == "-ns") targetNamespace = args[i + 1];
                //if (args[i] == "-out") outputDirectory = args[i + 1];
                //if (args[i] == "-tn") tableNameFilter = args[i + 1];
            }
        }

        public void Run()
        {
            var log = log4net.LogManager.GetLogger(typeof(DBCrudTester));
            string baseLogMessage = "[DBCrudTester]";


            if (string.IsNullOrEmpty(connectionString))
            {
                LogError("Connection string is not provided. Use -cs to specify it.");
                return;
            }

            using var conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
            conn.Open();
            LogInfo($"Connected to database with connection string: {connectionString}");

            try
            {
                CacheRefreshScheduler.Start(conn, log, TimeSpan.FromSeconds(10), "AutoCacheRefresh");

                conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                log.Info($"{baseLogMessage} Transaction started.");
                LogInfo("Transaction started. All operations will be rolled back at the end.");

                ManualTest(conn, log, baseLogMessage);
                ManualTest2(conn, log, baseLogMessage);

                //AutomaticTestOfAllClasses(conn, log, baseLogMessage);

                LogInfo();
                LogInfo("All tests completed.");
                log.Info("All tests completed.");
            }
            finally
            {
                conn.Rollback();
                LogInfo("DB rolled back");
                LogInfo();
                CacheRefreshScheduler.Stop();
            }
        }

        private void ManualTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, log4net.ILog log, string baseLogMessage)
        {
            MfcConvMovements mov = new MfcConvMovements()
            {
                StartType = 2,
                StartPar1 = 1001,
                DestType = 6,
                DestPar1 = 2002,
                ActualType = 2,
                ActualPar1 = 1001,
                OidUdm = 100,
                Constraint = "NORM",
                Priority = 1,
                Result = 0,                
            };

            //DateTime start = DateTime.Now;
            //LogResult(mov.Insert(conn, log, baseLogMessage), "    ManualInsert MfcConvMovements");
            //LogInfo($"OID: {mov.Oid}, DT_INSERT: {mov.DtInsert}");
            log.Info($"Insert: {(mov.Insert(conn, log, baseLogMessage) ? "OK" : "KO")} at DT_INSERT: {mov.DtInsert}");
            //LogWarning($"Insert took {(DateTime.Now - start).TotalMilliseconds:N2} ms");

            mov.ActualType = 6;
            mov.ActualPar1 = 2002;
            
            //start = DateTime.Now;
            //LogResult(mov.Update(conn, log, baseLogMessage), "    ManualUpdate ManToCom");
            //LogInfo($"DtUpdated: {mov.DtUpdate}");
            log.Info($"Update: {(mov.Update(conn, log, baseLogMessage) ? "OK" : "KO")} at DT_UPDATE: {mov.DtUpdate}");
            //LogWarning($"Update took {(DateTime.Now - start).TotalMilliseconds:N2} ms");

            //start = DateTime.Now;
            MfcConvMovements mov2 = MfcConvMovements.Get(conn, log, baseLogMessage, x => x.Oid == mov.Oid);
            //LogWarning($"ManualLoad took {(DateTime.Now - start).TotalMilliseconds:N2} ms");
            //LogResult(mov2 != null, "    ManualLoad ManToCom");
            //LogResult(mov2 != null && mov2.Oid == mov.Oid, "ManualLoad have the same OID of ManualInsert");
            //LogInfo($"DB Loaded actual position: {mov2.ActualPar1} DtUpdate: {mov2.DtUpdate}");
            log.Info($"Get: {(mov2 != null? "OK" : "KO")} at DT_UPDATE: {mov2.DtUpdate}");

            //start = DateTime.Now;
            var allMtc = MfcConvMovements.GetMany(conn, log, baseLogMessage);
            ////LogWarning($"ManualLoadAll took {(DateTime.Now - start).TotalMilliseconds:N2} ms");
            //LogResult(allMtc?.Count() != 0, "    ManualLoadAll MfcConvMovements");
            //LogInfo($"Loaded {allMtc?.Count()} MfcConvManToCom records from DB");
            log.Info($"GetMany: {(allMtc?.Count() != 0 ? "OK" : "KO")}");

            //LogResult(mov.Delete(conn, log, baseLogMessage), "    ManualDelete ManToCom");
            log.Info($"Delete: {(mov.Delete(conn, log, baseLogMessage) ? "OK" : "KO")}");
        }

        private void ManualTest2(Oracle.ManagedDataAccess.Client.OracleConnection conn, log4net.ILog log, string baseLogMessage)
        {
            MfcConvRouting.LoadCache(conn, log, baseLogMessage);

            int i = 0;
            List<TimeSpan> dbLoadAll = new();
            List<TimeSpan> dbLoadAll1001Linq = new();
            List<TimeSpan> dbLoadAll1001Sql = new();
            List<TimeSpan> dbLoad1001 = new();
            List<TimeSpan> cacheLoadAll = new();
            List<TimeSpan> cacheLoadAll1001 = new();
            List<TimeSpan> cacheLoad1001 = new();

            var pars = new Dictionary<string, object?>()
            {
                [":cdItemFrom"] = "1001",
            };
            int maxCount = 10;
            LogInfo($"Looping {maxCount} for extended test...");
            while (i < maxCount)
            {
                i++;

                // DB
                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage, ignoreCache: true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoadAll.Add(ts);
                    //LogInfo($"DB GetMany [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage, x => x.CdItemFrom == "1001", ignoreCache: true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoadAll1001Linq.Add(ts);
                    //LogInfo($"DB GetMany 1001 LINQ [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage, "CD_ITEM_FROM = :cdItemFrom", pars );
                    //var dbRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage, "CD_ITEM_FROM = 1001");
                    var ts = (DateTime.Now - dtStart);
                    dbLoadAll1001Sql.Add(ts);
                    //LogInfo($"DB GetMany 1001 SQL [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    MfcConvRouting dbRouting = MfcConvRouting.Get(conn, log, baseLogMessage, x => x.CdItemFrom == "1001", true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoad1001.Add(ts);
                    //LogInfo($"DB Get 1001 took {ts.TotalMilliseconds:N2} ms");
                }

                // Cache
                {
                    var dtStart = DateTime.Now;
                    var cacheRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage); 
                    var ts = (DateTime.Now - dtStart);
                    cacheLoadAll.Add(ts);
                    //LogInfo($"Cache GetMany [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    var cacheRouting = MfcConvRouting.GetMany(conn, log, baseLogMessage, x => x.CdItemFrom == "1001");
                    var ts = (DateTime.Now - dtStart);
                    cacheLoadAll1001.Add(ts);
                    //LogInfo($"Cache GetMany 1001 [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    MfcConvRouting cacheRouting = MfcConvRouting.Get(conn, log, baseLogMessage, x => x.CdItemFrom == "1001");
                    var ts = (DateTime.Now - dtStart);
                    cacheLoad1001.Add(ts);
                    //LogInfo($"Cache Get 1001 took {ts.TotalMilliseconds:N2} ms");
                }
            }

            LogInfo($"Summary of Get Tests:");
            LogInfo($"Source            | Min        | Max        | Avg        ");
            LogInfo($"DB GetMany        | {dbLoadAll.Min().TotalMilliseconds,7:N2} ms | {dbLoadAll.Max().TotalMilliseconds,7:N2} ms | {dbLoadAll.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"DB GetMany 1001   | {dbLoadAll1001Linq.Min().TotalMilliseconds,7:N2} ms | {dbLoadAll1001Linq.Max().TotalMilliseconds,7:N2} ms | {dbLoadAll1001Linq.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"DB GetMany 1001SQL| {dbLoadAll1001Sql.Min().TotalMilliseconds,7:N2} ms | {dbLoadAll1001Sql.Max().TotalMilliseconds,7:N2} ms | {dbLoadAll1001Sql.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"DB Get 1001       | {dbLoad1001.Min().TotalMilliseconds,7:N2} ms | {dbLoad1001.Max().TotalMilliseconds,7:N2} ms | {dbLoad1001.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"Cache GetMany     | {cacheLoadAll.Min().TotalMilliseconds,7:N2} ms | {cacheLoadAll.Max().TotalMilliseconds,7:N2} ms | {cacheLoadAll.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"Cache GetMany 1001| {cacheLoadAll1001.Min().TotalMilliseconds,7:N2} ms | {cacheLoadAll1001.Max().TotalMilliseconds,7:N2} ms | {cacheLoadAll1001.Average(x => x.TotalMilliseconds),7:N2} ms");
            LogInfo($"Cache Get 1001    | {cacheLoad1001.Min().TotalMilliseconds,7:N2} ms | {cacheLoad1001.Max().TotalMilliseconds,7:N2} ms | {cacheLoad1001.Average(x => x.TotalMilliseconds),7:N2} ms");
        }

        private void AutomaticTestOfAllClasses(Oracle.ManagedDataAccess.Client.OracleConnection conn, log4net.ILog log, string baseLogMessage)
        {
            var baseType = typeof(DBDataLibrary.CRUD.ACrudBase<>);

            // Cerca tutte le classi concrete che ereditano da ACrudBase<TData>
            List<Type> typesToTest = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    !t.IsAbstract &&
                    t.BaseType != null &&
                    t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == baseType)
                .ToList();

            LogInfo($"Found {typesToTest.Count} tables to test");

            Console.Write("Proceed? (Y/N): ");
            var proceed = Console.ReadLine()?.Trim().ToUpper();
            if (proceed != "Y")
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }

            int count = 1;

            foreach (var type in typesToTest)
            {

                try
                {
                    dynamic crudInstance = Activator.CreateInstance(type)!;

                    LogInfo($"[{count++}/{typesToTest.Count}] {type.Name} [{crudInstance.TableName}]:", ConsoleColor.White);

                    // GET THE CUSTOM ATTRIBUTE
                    // Assuming the TableTypes attribute is defined on the class itself
                    var tableTypeAttribute = type.GetCustomAttributes(typeof(TableTypeAttribute), false)
                                                .FirstOrDefault() as TableTypeAttribute;

                    if (tableTypeAttribute == null)
                    {
                        LogWarning($"    Skipping {type.Name}: No TableTypes attribute found.");
                        continue;
                    }

                    TableTypes tableType = tableTypeAttribute.TableType;

                    Log2Colors($"    TableType", $"{tableType}", secondColor: ConsoleColor.White);

                    // CONDITIONAL EXECUTION BASED ON ATTRIBUTE FLAGS

                    // INSERT
                    InsertTest(conn, type, crudInstance, log, baseLogMessage);

                    // UPDATE modifica la prima proprietà modificabile

                    UpdateTest(conn, type, crudInstance, log, baseLogMessage);

                    // LOAD
                    LoadTest(conn, type, tableType, log, baseLogMessage);

                    // DELETE
                    DeleteTest(conn, crudInstance, log, baseLogMessage);
                }
                catch (Exception ex)
                {
                    LogError($"    Error testing {type.Name}: {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}");
                }
            }
        }

        private void DeleteTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, dynamic crudInstance, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                bool deleted = crudInstance.Delete(conn, log, baseLogMessage);
                LogResult(deleted, "    Delete");
            }
            catch (Exception ex)
            {
                Log2Colors($"    Delete", ex.Message);
            }
        }

        private void InsertTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, dynamic crudInstance, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                bool inserted = crudInstance.Insert(conn, log, baseLogMessage);
                LogResult(inserted, $"    Insert");
                if (inserted)
                {
                    Dictionary<string, object> kvp = crudInstance.GetKeyValues();
                    var keys = string.Join(", ", kvp.Select(k =>
                    {
                        return $"{k.Key} = {k.Value}";
                    }));
                    LogInfo($"      -Inserted {type.Name}: {keys}");
                }
            }
            catch (Exception ex)
            {
                Log2Colors($"    Insert", ex.Message);
            }
        }

        private void UpdateTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, dynamic crudInstance, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                PropertyInfo modProp = null;
                foreach (var prop in type.GetProperties())
                {
                    if (prop.CanWrite && prop.PropertyType == typeof(string))
                    {
                        modProp = prop;
                        break;
                    }
                }
                if (modProp != null)
                {
                    modProp.SetValue(crudInstance, "TestValue");
                    bool updated = crudInstance.Update(conn, log, baseLogMessage);
                    LogResult(updated, "    Update");
                }
            }
            catch (Exception ex)
            {
                Log2Colors($"    Update", ex.Message);
            }
        }

        private void LoadTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, TableTypes tableType, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                var crudBaseGeneric = typeof(ACrudBase<>).MakeGenericType(type); // this works because ACrudBase<T> is open

                //var method = crudBaseGeneric.GetMethod("GetMany", BindingFlags.Public | BindingFlags.Static);
                var methods = crudBaseGeneric.GetMethods(BindingFlags.Public | BindingFlags.Static);
                var method = methods.FirstOrDefault(x => x.Name == "GetMany");
                if (method != null)
                {
                    DateTime dtStart = DateTime.Now;
                    var result = method.Invoke(null, new object[] { conn, log, baseLogMessage }); // second param is whereFilter, third parameter il cache loading
                    var loadedList = ((IEnumerable<object>)result)?.ToList();
                    LogResult(loadedList?.Count ?? 0, "    Get");
                    LogInfo($"    GetMany took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                }
                else
                {
                    LogWarning($"    GetMany not found for type {type.Name}");
                }

                // CACHED Table test
                if ((tableType & TableTypes.Cached) == TableTypes.Cached)
                {
                    if (method != null)
                    {
                        // Provo un accesso alla cache
                        DateTime dtStart = DateTime.Now;
                        var result = method.Invoke(null, new object[] { conn, log, baseLogMessage }); // carico dalla cache
                        var loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    CacheLoad");
                        LogInfo($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                        // Provo un reload della cache
                        var loadCacheMethod = methods.FirstOrDefault(x => x.Name == "LoadCache");
                        dtStart = DateTime.Now;
                        result = loadCacheMethod.Invoke(null, new object[] { conn, log, baseLogMessage}); // ricarico la cache
                        result = method.Invoke(null, new object[] { conn, log, baseLogMessage }); // carico dalla cache
                        loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    CacheReLoad");
                        LogInfo($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                    }
                }
            }
            catch (Exception ex)
            {
                Log2Colors($"    Get", ex.Message);
            }
        }


        private void LogTime()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now:yyyy/MM/dd-HH:mm:ss.fff}] ");
            Console.ResetColor();
        }

        private void Log2Colors(string firstColotText, string secondColorText, 
            ConsoleColor firstColor = ConsoleColor.Gray, 
            ConsoleColor secondColor = ConsoleColor.Yellow)
        {
            LogTime();
            Console.ResetColor(); 
            Console.ForegroundColor = firstColor;
            Console.Write($"[INFO]  {firstColotText} : ");
            Console.ForegroundColor = secondColor;            
            Console.WriteLine(secondColorText);
            Console.ResetColor();
        }

        private void LogResult(int number, string operation)
        {
            LogTime();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            if (number > 0)
            {
                Console.Write($"[INFO]  {operation} : ");
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.Write($"[INFO]  {operation} : ");
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(number);
            Console.ResetColor();
        }

        private void LogResult(bool result, string operation)
        {
            LogTime();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            if (result)
            {
                Console.Write($"[INFO]  {operation} : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"[ERROR] {operation} : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed");
                Console.ResetColor();
            }
        }

        private void LogColor(string message, ConsoleColor txtColor = ConsoleColor.Gray)
        {
            LogTime();
            Console.ForegroundColor = txtColor;
            Console.WriteLine($"{message}");
            Console.ResetColor();
        }

        private void LogInfo(string message = "", ConsoleColor txtColor = ConsoleColor.White)
        {
            LogColor($"[INFO]  {message}", txtColor);
        }

        private void LogError(string message)
        {
            LogColor($"[ERROR] {message}", ConsoleColor.Red);
        }

        private void LogWarning(string message)
        {
            LogColor($"[WARN]  {message}", ConsoleColor.Yellow);
        }
    }
}
