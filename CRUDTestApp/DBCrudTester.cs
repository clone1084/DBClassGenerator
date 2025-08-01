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

                var baseType = typeof(DBDataLibrary.CRUD.ACrudBase<,>);

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
                        else
                        {
                            Log2Colors($"    Insert", "SKIPPED");
                        }

                        // UPDATE modifica la prima proprietà modificabile

                        if ((tableType & TableTypes.Updatable) == TableTypes.Updatable)
                        {
                            var data = crudInstance.GetData();
                            var dataType = data.GetType();
                            PropertyInfo modProp = null;
                            foreach (var prop in dataType.GetProperties())
                            {
                                if (prop.CanWrite && prop.PropertyType == typeof(string))
                                {
                                    modProp = prop;
                                    break;
                                }
                            }
                            if (modProp != null)
                            {
                                modProp.SetValue(data, "TestValue");
                                bool updated = crudInstance.Update(conn);
                                LogResult(updated, "    Update");
                            }
                        }
                        else
                        {
                            Log2Colors($"    Update", "SKIPPED");
                        }

                        // LOAD
                        // Will be executed for all types
                        {
                            var loaded = crudInstance.LoadAll(conn);
                            LogResult(loaded.Count, "    Load");
                        }

                        // DELETE
                        if ((tableType & TableTypes.Deletable) == TableTypes.Deletable)
                        {
                            bool deleted = crudInstance.Delete(conn);
                            LogResult(deleted, "    Delete");
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
