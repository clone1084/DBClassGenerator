using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using DBDataLibrary.Tables;
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
                conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                LogInfo("Transaction started. All operations will be rolled back at the end.");

                ManualTest(conn, log, baseLogMessage);
                ManualTest2(conn, log, baseLogMessage);

                //AutomaticTestOfAllClasses(conn, log, baseLogMessage);

                LogInfo();
                LogInfo("All tests completed.");
            }
            finally
            {
                conn.Rollback();
                LogInfo("DB rolled back");
                LogInfo();
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
            LogResult(mov.Insert(conn, log, baseLogMessage), "    ManualInsert MfcConvMovements");
            LogInfo($"OID: {mov.Oid}, DT_INSERT: {mov.DtInsert}");
            //LogWarning($"Insert took {(DateTime.Now - start).TotalMilliseconds:N2} ms");

            mov.ActualType = 6;
            mov.ActualPar1 = 2002;
            
            //start = DateTime.Now;
            LogResult(mov.Update(conn, log, baseLogMessage), "    ManualUpdate ManToCom");
            LogInfo($"DtUpdated: {mov.DtUpdate}");
            //LogWarning($"Update took {(DateTime.Now - start).TotalMilliseconds:N2} ms");

            //start = DateTime.Now;
            MfcConvMovements mov2 = MfcConvMovements.Load(conn, log, baseLogMessage, x => x.Oid == mov.Oid);
            //LogWarning($"ManualLoad took {(DateTime.Now - start).TotalMilliseconds:N2} ms");
            LogResult(mov2 != null, "    ManualLoad ManToCom");
            LogResult(mov2 != null && mov2.Oid == mov.Oid, "ManualLoad have the same OID of ManualInsert");
            LogInfo($"DB Loaded actual position: {mov2.ActualPar1} DtUpdate: {mov2.DtUpdate}");

            //start = DateTime.Now;
            var allMtc = MfcConvMovements.LoadAll(conn, log, baseLogMessage);
            //LogWarning($"ManualLoadAll took {(DateTime.Now - start).TotalMilliseconds:N2} ms");
            LogResult(allMtc?.Count() != 0, "    ManualLoadAll MfcConvMovements");
            LogInfo($"Loaded {allMtc?.Count()} MfcConvManToCom records from DB");

            LogResult(mov.Delete(conn, log, baseLogMessage), "    ManualDelete ManToCom");
        }

        private void ManualTest2(Oracle.ManagedDataAccess.Client.OracleConnection conn, log4net.ILog log, string baseLogMessage)
        {
            MfcConvRouting.LoadCache(conn, log, baseLogMessage);

            int i = 0;
            List<TimeSpan> dbLoadAll = new();
            List<TimeSpan> dbLoadAll1001 = new();
            List<TimeSpan> dbLoad1001 = new();
            List<TimeSpan> cacheLoadAll = new();
            List<TimeSpan> cacheLoadAll1001 = new();
            List<TimeSpan> cacheLoad1001 = new();

            while (i < 100)
            {
                i++;

                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.LoadAll(conn, log, baseLogMessage, true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoadAll.Add(ts);
                    LogInfo($"DB LoadAll [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.LoadAll(conn, log, baseLogMessage, x => x.CdItemFrom == "1001", true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoadAll1001.Add(ts);
                    LogInfo($"DB LoadAll 1001 [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    MfcConvRouting dbRouting = MfcConvRouting.Load(conn, log, baseLogMessage, x => x.CdItemFrom == "1001", true);
                    var ts = (DateTime.Now - dtStart);
                    dbLoad1001.Add(ts);
                    LogInfo($"DB Load 1001 took {ts.TotalMilliseconds:N2} ms");
                }

                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.LoadAll(conn, log, baseLogMessage);
                    var ts = (DateTime.Now - dtStart);
                    cacheLoadAll.Add(ts);
                    LogInfo($"Cache LoadAll [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    var dbRouting = MfcConvRouting.LoadAll(conn, log, baseLogMessage, x => x.CdItemFrom == "1001");
                    var ts = (DateTime.Now - dtStart);
                    cacheLoadAll1001.Add(ts);
                    LogInfo($"Cache LoadAll 1001 [{dbRouting.Count()}] took {ts.TotalMilliseconds:N2} ms");
                }
                {
                    var dtStart = DateTime.Now;
                    MfcConvRouting cacheRouting = MfcConvRouting.Load(conn, log, baseLogMessage, x => x.CdItemFrom == "1001");
                    var ts = (DateTime.Now - dtStart);
                    cacheLoad1001.Add(ts);
                    LogInfo($"Cache Load 1001 took {ts.TotalMilliseconds:N2} ms");
                }
            }

            LogInfo($"Summary of Load Tests:");
            LogInfo($"Source            | Min     | Max     | Avg     ");
            LogInfo($"DB LoadAll        | {dbLoadAll.Min().TotalMilliseconds,7:N2} | {dbLoadAll.Max().TotalMilliseconds,7:N2} | {dbLoadAll.Average(x => x.TotalMilliseconds),7:N2}");
            LogInfo($"DB LoadAll 1001   | {dbLoadAll1001.Min().TotalMilliseconds,7:N2} | {dbLoadAll1001.Max().TotalMilliseconds,7:N2} | {dbLoadAll1001.Average(x => x.TotalMilliseconds),7:N2}");
            LogInfo($"DB Load 1001      | {dbLoad1001.Min().TotalMilliseconds,7:N2} | {dbLoad1001.Max().TotalMilliseconds,7:N2} | {dbLoad1001.Average(x => x.TotalMilliseconds),7:N2}");
            LogInfo($"Cache LoadAll     | {cacheLoadAll.Min().TotalMilliseconds,7:N2} | {cacheLoadAll.Max().TotalMilliseconds,7:N2} | {cacheLoadAll.Average(x => x.TotalMilliseconds),7:N2}");
            LogInfo($"Cache LoadAll 1001| {cacheLoadAll1001.Min().TotalMilliseconds,7:N2} | {cacheLoadAll1001.Max().TotalMilliseconds,7:N2} | {cacheLoadAll1001.Average(x => x.TotalMilliseconds),7:N2}");
            LogInfo($"Cache Load 1001   | {cacheLoad1001.Min().TotalMilliseconds,7:N2} | {cacheLoad1001.Max().TotalMilliseconds,7:N2} | {cacheLoad1001.Average(x => x.TotalMilliseconds),7:N2}");
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

                //var method = crudBaseGeneric.GetMethod("LoadAll", BindingFlags.Public | BindingFlags.Static);
                var method = crudBaseGeneric.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x => x.Name == "LoadAll");
                if (method != null)
                {
                    DateTime dtStart = DateTime.Now;
                    var result = method.Invoke(null, new object[] { conn, log, baseLogMessage, false }); // second param is whereFilter, third parameter il cache loading
                    var loadedList = ((IEnumerable<object>)result)?.ToList();
                    LogResult(loadedList?.Count ?? 0, "    Load");
                    LogInfo($"    Load took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                }
                else
                {
                    LogWarning($"    LoadAll not found for type {type.Name}");
                }

                // CACHED Table test
                if ((tableType & TableTypes.Cached) == TableTypes.Cached)
                {
                    if (method != null)
                    {
                        // Provo un accesso alla cache
                        DateTime dtStart = DateTime.Now;
                        var result = method.Invoke(null, new object[] { conn, log, baseLogMessage, false }); // second param is whereFilter, third parameter il cache loading
                        var loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    CacheLoad");
                        LogInfo($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                        // Provo un reload della cache
                        dtStart = DateTime.Now;
                        result = method.Invoke(null, new object[] { conn, log, baseLogMessage, true }); // second param is whereFilter, third parameter il cache loading
                        loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    CacheReLoad");
                        LogInfo($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                        //// Provo un accesso concorrente alla cache
                        //LogInfo("    Avvio test di accesso concorrente alla cache...");
                        //var taskReload = Task.Run(() =>
                        //{
                        //    // Reload della cache
                        //    DateTime dtStart = DateTime.Now;
                        //    var result = method.Invoke(null, new object[] { conn, log, baseLogMessage, true });
                        //    var loadedList = ((IEnumerable<object>)result)?.ToList();
                        //    LogResult(loadedList?.Count ?? 0, "    Task CacheReLoad");
                        //    LogInfo($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                        //});

                        //var taskAccess = Task.Run(() =>
                        //{
                        //    // Accesso ai dati (senza reload)
                        //    DateTime dtStart = DateTime.Now;
                        //    var result = method.Invoke(null, new object[] { conn, log, baseLogMessage, false });
                        //    var loadedList = ((IEnumerable<object>)result)?.ToList();
                        //    LogResult(loadedList?.Count ?? 0, "    Task CacheAccess");
                        //    LogInfo($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                        //});

                        //Task.WhenAll(taskReload, taskAccess).Wait();
                        //LogInfo("    Test di accesso concorrente completato.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log2Colors($"    Load", ex.Message);
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
