using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using DBDataLibrary.Tables;
using System;
using System.Collections.Generic;
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
                        if ((tableType & TableTypes.Insertable) == TableTypes.Insertable)
                        {
                            InsertTest(conn, type, crudInstance);
                        }
                        else
                        {
                            Log2Colors($"    Insert", "SKIPPED");
                        }

                        // UPDATE modifica la prima proprietà modificabile

                        if ((tableType & TableTypes.Updatable) == TableTypes.Updatable)
                        {
                            UpdateTest(conn, type, crudInstance);
                        }
                        else
                        {
                            Log2Colors($"    Update", "SKIPPED");
                        }

                        // LOAD
                        // Will be executed for all types
                        {
                            LoadTest(conn, type, tableType);
                        }

                        // DELETE
                        if ((tableType & TableTypes.Deletable) == TableTypes.Deletable)
                        {
                            DeleteTest(conn, crudInstance);
                        }
                        else
                        {
                            Log2Colors($"    Delete", "SKIPPED");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError($"    Error testing {type.Name}: {ex.Message}");
                    }
                }
                
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

        private void DeleteTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, dynamic crudInstance)
        {
            bool deleted = crudInstance.Delete(conn);
            LogResult(deleted, "    Delete");
        }

        private void InsertTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, dynamic crudInstance)
        {
            bool inserted = crudInstance.Insert(conn);
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

        private void UpdateTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, dynamic crudInstance)
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
                bool updated = crudInstance.Update(conn);
                LogResult(updated, "    Update");
            }
        }

        private void LoadTest(Oracle.ManagedDataAccess.Client.OracleConnection conn, Type type, TableTypes tableType)
        {
            var crudBaseGeneric = typeof(ACrudBase<>).MakeGenericType(type); // this works because ACrudBase<T> is open
            var method = crudBaseGeneric.GetMethod("LoadAll", BindingFlags.Public | BindingFlags.Static);
            if (method != null)
            {
                DateTime dtStart = DateTime.Now;
                var result = method.Invoke(null, new object[] { conn, "", false }); // second param is whereFilter, third parameter il cache loading
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
                    var result = method.Invoke(null, new object[] { conn, "", false }); // second param is whereFilter, third parameter il cache loading
                    var loadedList = ((IEnumerable<object>)result)?.ToList();
                    LogResult(loadedList?.Count ?? 0, "    CacheLoad");
                    LogInfo($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                    // Provo un reload della cache
                    dtStart = DateTime.Now;
                    result = method.Invoke(null, new object[] { conn, "", true }); // second param is whereFilter, third parameter il cache loading
                    loadedList = ((IEnumerable<object>)result)?.ToList();
                    LogResult(loadedList?.Count ?? 0, "    CacheReLoad");
                    LogInfo($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");

                    // Provo un accesso concorrente alla cache
                    LogInfo("    Avvio test di accesso concorrente alla cache...");
                    var taskReload = Task.Run(() =>
                    {
                        // Reload della cache
                        DateTime dtStart = DateTime.Now;
                        var result = method.Invoke(null, new object[] { conn, "", true });
                        var loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    Task CacheReLoad");
                        LogInfo($"    CacheReLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                    });

                    var taskAccess = Task.Run(() =>
                    {
                        // Accesso ai dati (senza reload)
                        DateTime dtStart = DateTime.Now;
                        var result = method.Invoke(null, new object[] { conn, "", false });
                        var loadedList = ((IEnumerable<object>)result)?.ToList();
                        LogResult(loadedList?.Count ?? 0, "    Task CacheAccess");
                        LogInfo($"    CacheLoad took {(DateTime.Now - dtStart).TotalMilliseconds:N2} ms");
                    });

                    Task.WhenAll(taskReload, taskAccess).Wait();
                    LogInfo("    Test di accesso concorrente completato.");
                }
            }
        }

        private void Log2Colors(string firstColotText, string secondColorText, 
            ConsoleColor firstColor = ConsoleColor.Gray, ConsoleColor secondColor = ConsoleColor.Yellow)
        {
            Console.ResetColor();
            Console.ForegroundColor = firstColor;
            Console.Write($"[INFO]  {firstColotText} : ");
            Console.ForegroundColor = secondColor;            
            Console.WriteLine(secondColorText);
            Console.ResetColor();
        }

        private void LogResult(int number, string operation)
        {
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
            Console.ResetColor();
            Console.ForegroundColor = txtColor;
            Console.WriteLine($"{message}");
            Console.ResetColor();
        }

        private void LogInfo(string message = "", ConsoleColor txtColor = ConsoleColor.Gray)
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
