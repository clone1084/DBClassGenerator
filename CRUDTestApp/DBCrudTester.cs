using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using DBDataLibrary.Utils;
using log4net;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;

namespace CRUDTestApp
{
    internal class DBCrudTester
    {
        static string connectionString = "";
        //static string dataTablesDll = "";

        public DBCrudTester(string[] args)
        {
            ParseArgs(args);
        }

        static void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-cs") connectionString = args[i + 1];
                //if (args[i] == "-p") dataTablesDll = args[i + 1];
                //if (args[i] == "-ns") targetNamespace = args[i + 1];
                //if (args[i] == "-out") outputDirectory = args[i + 1];
                //if (args[i] == "-tn") tableNameFilter = args[i + 1];
            }
        }

        public void Run()
        {
            var log = LogManager.GetLogger(typeof(DBCrudTester));
            string baseLogMessage = "[DBCrudTester]";


            if (string.IsNullOrEmpty(connectionString))
            {
                log.Error("Connection string is not provided. Use -cs to specify it.");
                return;
            }

            //if (string.IsNullOrEmpty(dataTablesDll) || !File.Exists(dataTablesDll))
            //{
            //    log.Error("DataTable.dll is not provided or the file does not exist. Use -p to specify it.");
            //    return;
            //}

            //var asm = BuildAndLoadTablesProject(log, baseLogMessage, dataTablesDll);
            //if (asm == null)
            //{
            //    log.Error("Failed to build or load the project.");
            //    return;
            //}

            using var conn = new OracleConnection(connectionString);
            conn.Open();
            log.Info($"Connected to database with connection string: {connectionString}");

            try
            {
                log.Info($"{baseLogMessage} Starting CRUD tests...");

                CacheRefreshScheduler.Start(connectionString, log, TimeSpan.FromSeconds(10), "AutoCacheRefresh");

                conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                log.Info($"{baseLogMessage} Transaction started.");

                AutomaticTestOfAllClasses(conn, log, baseLogMessage);

                log.Info("All tests completed.");
            }
            finally
            {
                conn.Rollback();
                log.Info("DB rolled back");
                CacheRefreshScheduler.Stop();
            }
        }

        private void AutomaticTestOfAllClasses(OracleConnection conn, ILog log, string baseLogMsg)
        {
            baseLogMsg += "AutomaticTestOfAllClasses->";
            var baseType = typeof(ACrudBase<>);

            // Cerca tutte le classi concrete che ereditano da ACrudBase<TData>
            List<Type> typesToTest = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    !t.IsAbstract &&
                    t.BaseType != null &&
                    t.BaseType.IsGenericType &&
                    t.BaseType.GetGenericTypeDefinition() == baseType)
                .ToList();


            log.Info($"{baseLogMsg}Found {typesToTest.Count()} tables to test");

            Console.Write("Proceed? (Y/N): ");
            var proceed = Console.ReadLine()?.Trim().ToUpper();
            if (proceed != "Y")
            {
                log.Warn($"{baseLogMsg}Operation cancelled.");
                return;
            }

            int count = 1;

            foreach (var type in typesToTest)
            {

                try
                {
                    dynamic crudInstance = Activator.CreateInstance(type)!;

                    log.Info($"{baseLogMsg}[{count++}/{typesToTest.Count()}] {type.Name} [{crudInstance.TableName}]:");

                    // GET THE CUSTOM ATTRIBUTE
                    // Assuming the TableTypes attribute is defined on the class itself
                    var tableTypeAttribute = type.GetCustomAttributes(typeof(TableTypeAttribute), false)
                                                .FirstOrDefault() as TableTypeAttribute;

                    if (tableTypeAttribute == null)
                    {
                        log.Warn($"{baseLogMsg}    Skipping {type.Name}: No TableTypes attribute found.");
                        continue;
                    }

                    TableTypes tableType = tableTypeAttribute.TableType;

                    log.Info($"{baseLogMsg}    TableType: {tableType}");

                    if (tableType.HasFlag(TableTypes.Insertable))
                    {
                        // INSERT
                        InsertTest(conn, type, crudInstance, log, baseLogMsg);
                    }
                    else
                    {
                        log.Info($"    Insert UNAVAILABLE");
                    }

                    if (tableType.HasFlag(TableTypes.Updatable))
                    {
                        // UPDATE modifica la prima proprietà modificabile
                        UpdateTest(conn, type, crudInstance, log, baseLogMsg);
                    }
                    else
                    {
                        log.Info($"    Update UNAVAILABLE");
                    }

                    // LOAD
                    LoadTest(conn, type, tableType, log, baseLogMsg);

                    if (tableType.HasFlag(TableTypes.Deletable))
                    {
                        // DELETE
                        DeleteTest(conn, crudInstance, log, baseLogMsg);
                    }
                    else
                    {
                        log.Info($"    Delete UNAVAILABLE");
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{baseLogMsg}    Error testing {type.Name}: {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}");
                }
            }
        }

        private void DeleteTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, dynamic crudInstance, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                bool deleted = crudInstance.Delete(conn, log, baseLogMessage);
                log.Info($"    Delete {(deleted? "OK": "KO")}");
            }
            catch (Exception ex)
            {
                log.Error($"    Delete: {ex.Message}", ex);
            }
        }

        private void InsertTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, dynamic crudInstance, log4net.ILog log, string baseLogMessage)
        {
            try
            {
                bool inserted = crudInstance.Insert(conn, log, baseLogMessage);
                log.Info($"    Insert {(inserted ? "OK" : "KO")}");

                if (inserted)
                {
                    Dictionary<string, object> kvp = crudInstance.GetKeyValues();
                    var keys = string.Join(", ", kvp.Select(k =>
                    {
                        return $"{k.Key} = {k.Value}";
                    }));
                    log.Info($"      -Inserted {type.Name}: {keys}");
                }
            }
            catch (Exception ex)
            {
                log.Error($"    Insert: {ex.Message}", ex);
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
                    log.Info($"    Update {(updated ? "OK" : "KO")}");
                }
            }
            catch (Exception ex)
            {
                log.Info($"    Update: {ex.Message}", ex);
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
                    log.Info($"    GetMany {(loadedList?.Count == 0? "KO" : "OK")}");
                    log.Info($"    GetMany took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                }
                else
                {
                    log.Warn($"    GetMany not found for type {type.Name}");
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
                        log.Info($"    CacheLoad {(loadedList?.Count == 0 ? "KO" : "OK")}");
                        log.Info($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                        // Provo un reload della cache
                        var loadCacheMethod = methods.FirstOrDefault(x => x.Name == "LoadCache");
                        dtStart = DateTime.Now;
                        result = loadCacheMethod.Invoke(null, new object[] { conn, log, baseLogMessage}); // ricarico la cache
                        result = method.Invoke(null, new object[] { conn, log, baseLogMessage }); // carico dalla cache
                        loadedList = ((IEnumerable<object>)result)?.ToList();
                        log.Info($"    CacheReLoad {(loadedList?.Count == 0 ? "KO" : "OK")}");
                        log.Info($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"    GetMany: {ex.Message}", ex);
            }
        }


        //private void LogTime()
        //{
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //    Console.Write($"[{DateTime.Now:yyyy/MM/dd-HH:mm:ss.fff}] ");
        //    Console.ResetColor();
        //}

        //private void log.Info(string firstColotText, string secondColorText, 
        //    ConsoleColor firstColor = ConsoleColor.Gray, 
        //    ConsoleColor secondColor = ConsoleColor.Yellow)
        //{
        //    LogTime();
        //    Console.ResetColor(); 
        //    Console.ForegroundColor = firstColor;
        //    Console.Write($"[INFO]  {firstColotText} : ");
        //    Console.ForegroundColor = secondColor;            
        //    Console.WriteLine(secondColorText);
        //    Console.ResetColor();
        //}

        //private void LogResult(int number, string operation)
        //{
        //    LogTime();
        //    Console.ResetColor();
        //    Console.ForegroundColor = ConsoleColor.White;
        //    if (number > 0)
        //    {
        //        Console.Write($"[INFO]  {operation} : ");
        //        Console.ForegroundColor = ConsoleColor.Green;
        //    }
        //    else
        //    {
        //        Console.Write($"[INFO]  {operation} : ");
        //        Console.ForegroundColor = ConsoleColor.Red;
        //    }
        //    Console.WriteLine(number);
        //    Console.ResetColor();
        //}

        //private void LogResult(bool result, string operation)
        //{
        //    LogTime();
        //    Console.ResetColor();
        //    Console.ForegroundColor = ConsoleColor.White;
        //    if (result)
        //    {
        //        Console.Write($"[INFO]  {operation} : ");
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("OK");
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        Console.Write($"[ERROR] {operation} : ");
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("Failed");
        //        Console.ResetColor();
        //    }
        //}

        //private void LogColor(string message, ConsoleColor txtColor = ConsoleColor.Gray)
        //{
        //    LogTime();
        //    Console.ForegroundColor = txtColor;
        //    Console.WriteLine($"{message}");
        //    Console.ResetColor();
        //}

        //private void log.Info(string message = "", ConsoleColor txtColor = ConsoleColor.White)
        //{
        //    LogColor($"[INFO]  {message}", txtColor);
        //}

        //private void log.Error(string message)
        //{
        //    LogColor($"[ERROR] {message}", ConsoleColor.Red);
        //}

        //private void log.Warn(string message)
        //{
        //    LogColor($"[WARN]  {message}", ConsoleColor.Yellow);
        //}
    }
}
