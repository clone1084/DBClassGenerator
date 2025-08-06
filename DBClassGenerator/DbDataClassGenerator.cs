using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClassGenerator
{
    internal static class DbDataClassGenerator
    {
        static string connectionString = "";
        static string targetNamespace = "GeneratedNamespace";
        static string outputDirectory = Directory.GetCurrentDirectory();
        static string tableNameFilter = "";

        public static void GenerateClasses(string[] args)
        {
            if (!ParseArgs(args))
            {
                return;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Missing connection string. Use -cs <connectionString>");
                return;
            }

            if (string.IsNullOrEmpty(tableNameFilter))
            {
                Console.WriteLine("Enter a table name or filter pattern (SQL LIKE syntax, e.g. 'EMP%'):");
                // Sostituisci questa riga:
                // tableNameFilter = Console.ReadLine();
                // Con questa versione che gestisce il possibile valore null:
                tableNameFilter = Console.ReadLine() ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(tableNameFilter))
            {
                Console.WriteLine("Filter cannot be empty.");
                return;
            }

            using var conn = new OracleConnection(connectionString);
            conn.Open();

            DateTime startTime = DateTime.Now;            

            var tables = GetTables(conn, tableNameFilter);

            int tableCount = tables.Count;

            Console.WriteLine($"Found {tableCount} tables matching filter '{tableNameFilter}'");

            Console.Write("Proceed? (Y/N): ");
            var proceed = Console.ReadLine()?.Trim().ToUpper();
            if (proceed != "Y")
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }

            int tableIndex = 1;
            foreach (var tableName in tables)
            {
                Console.WriteLine($"({tableIndex++}/{tableCount}) {tableName}:");

                Directory.CreateDirectory(outputDirectory);

                //var classPath = Path.Combine(outputDirectory, $"Tables");
                //Directory.CreateDirectory(classPath);

                var columns = GetColumns(conn, tableName);
                var classCode = GenerateClassCode(tableName, columns, conn);

                File.WriteAllText(Path.Combine(outputDirectory, $"{NormalizeName(tableName)}.table.cs"), classCode);
                Console.WriteLine($"    Generated: {NormalizeName(tableName)}.cs");

                //var definesPath = Path.Combine(outputDirectory, $"Defines");
                //Directory.CreateDirectory(definesPath);
                string tableDefineFileName = Path.Combine(outputDirectory, $"{NormalizeName(tableName)}.define.cs");
                if (!File.Exists(tableDefineFileName))
                {
                    var defineClassCode = GenerateDefineClassCode(tableName);
                    File.WriteAllText(tableDefineFileName, defineClassCode);
                    Console.WriteLine($"    Generated: {NormalizeName(tableName)}.define.cs");
                }

                //var extensionPath = Path.Combine(outputDirectory, $"Extensions");
                //Directory.CreateDirectory(extensionPath);
                string extensionFileName = Path.Combine(outputDirectory, $"{NormalizeName(tableName)}.custom.cs");
                if (!File.Exists(extensionFileName))
                {
                    var extensionClassCode = GenerateExtensionClassCode(tableName);
                    File.WriteAllText(extensionFileName, extensionClassCode);
                    Console.WriteLine($"    Generated: {NormalizeName(tableName)}.custom.cs");
                }
            }

            Console.WriteLine($"---");
            Console.WriteLine($"Generated {tableCount} classes in {DateTime.Now.Subtract(startTime).TotalSeconds:N3} s");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static bool ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DbDataClassGenerator -cs <connectionString> -ns <namespace> -out <outputDirectory> -tn <tableNameFilter>");
                Console.WriteLine("Example: DbDataClassGenerator -cs \"User Id=myuser;Password=mypassword;Data Source=mydatasource\" -ns MyNamespace -out ./GeneratedClasses -tn EMP%");
                return false;
            }

            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-cs") connectionString = args[i + 1];
                if (args[i] == "-ns") targetNamespace = args[i + 1];
                if (args[i] == "-out") outputDirectory = args[i + 1];
                if (args[i] == "-tn") tableNameFilter = args[i + 1];
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Console.WriteLine($"ConnectionString: {connectionString}");
            Console.WriteLine($"TableName(s): {tableNameFilter}");
            Console.WriteLine($"NameSpace: {targetNamespace}");
            Console.WriteLine($"OutputDirectory: {outputDirectory}");

            return true;
        }

        static List<string> GetTables(OracleConnection conn, string filter)
        {
            var list = new List<string>();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT table_name FROM user_tables WHERE table_name LIKE :filter ORDER BY table_name";
            cmd.Parameters.Add(new OracleParameter(":filter", filter.ToUpper()));
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(reader.GetString(0));
            return list;
        }

        class ColumnInfo
        {
            public string Name = "";
            public string DataType = "";
            public bool IsNullable;
            public int? Precision;
            public int? Scale;
            public string? DefaultValue;
            public bool IsIdentity { get; set; }

        }

        static List<ColumnInfo> GetColumns(OracleConnection conn, string tableName)
        {
            var list = new List<ColumnInfo>();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT column_name, data_type, nullable, data_precision, data_scale, data_default
                                FROM user_tab_columns 
                                WHERE table_name = :tableName 
                                ORDER BY column_id";
            cmd.Parameters.Add(new OracleParameter(":tableName", tableName));
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ColumnInfo
                {
                    Name = reader.GetString(0),
                    DataType = reader.GetString(1),
                    IsNullable = reader.GetString(2) == "Y",
                    Precision = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Scale = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    DefaultValue = reader.IsDBNull(5) ? null : reader.GetString(5)?.Trim(),

                });
            }

            string identitySql = @"
                    SELECT COLUMN_NAME 
                    FROM USER_TAB_IDENTITY_COLS 
                    WHERE TABLE_NAME = :tableName";

            using var identityCmd = new OracleCommand(identitySql, conn);
            identityCmd.Parameters.Add(new OracleParameter("tableName", tableName));
            using var identityReader = identityCmd.ExecuteReader();
            var identityCols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            while (identityReader.Read())
            {
                identityCols.Add(identityReader.GetString(0));
            }

            // Ora aggiorna le colonne già lette
            foreach (var col in list)
            {
                if (identityCols.Contains(col.Name))
                    col.IsIdentity = true;
            }

            return list;
        }

        static string? ParseDefaultValue(string? oracleDefault, string csharpType)
        {
            if (string.IsNullOrWhiteSpace(oracleDefault)) return null;

            oracleDefault = oracleDefault.Trim();

            // Gestione costanti SQL note
            if (oracleDefault.Equals("SYSDATE", StringComparison.OrdinalIgnoreCase))
                return "DateTime.Now";

            // Stringhe delimitate da apici
            if (oracleDefault.StartsWith("'") && oracleDefault.EndsWith("'"))
            {
                string value = oracleDefault.Trim('\'').Replace("''", "'"); // Escape SQL
                return $"\"{value}\"";
            }

            // Numerici
            if (decimal.TryParse(oracleDefault, out var number))
            {
                return csharpType switch
                {
                    "decimal" or "decimal?" => $"{number}m",
                    "float" or "float?" => $"{number}f",
                    "double" or "double?" => $"{number}d",
                    _ => oracleDefault
                };
            }

            // Booleani in caso di colonne custom
            if (oracleDefault.Equals("1")) return "true";
            if (oracleDefault.Equals("0")) return "false";

            // Date generiche Oracle
            if (oracleDefault.StartsWith("TO_DATE", StringComparison.OrdinalIgnoreCase))
                return "DateTime.Now"; // fallback per ora

            // Altri casi: fallback nullo
            return null;
        }

        static string MapOracleTypeToCSharp(string oracleType, int? precision, int? scale, bool isNullable)
        {
            if (oracleType.Contains("("))
            {
                oracleType = oracleType.Substring(0, oracleType.IndexOf("("));
            }
            string type = oracleType switch
            {
                "NUMBER" => (scale ?? 0) > 0 ? "decimal"
                          : (precision ?? 0) <= 9 ? "int"
                          : (precision ?? 0) <= 18 ? "long"
                          : "decimal",

                "FLOAT" => "double",
                "BINARY_FLOAT" => "float",
                "BINARY_DOUBLE" => "double",
                "DATE" or "TIMESTAMP" => "DateTime",
                "CHAR" or "NCHAR" or "VARCHAR2" or "NVARCHAR2" or "CLOB" or "NCLOB" => "string",
                _ => "string"
            };

            //return (type != "string" && isNullable) ? type + "?" : type;		

            // Add '?' for value types if column is nullable
            bool isValueType = type is not "string" and not "byte[]" and not "object";
            return isValueType && isNullable ? $"{type}?" : type;
        }

        static string NormalizeName(string name)
        {
            var parts = name.ToLower().Split('_');
            var sb = new StringBuilder();
            foreach (var part in parts)
                sb.Append(char.ToUpper(part[0]) + part.Substring(1));
            return sb.ToString();
        }

        static string GenerateClassCode(string table, List<ColumnInfo> columns, OracleConnection conn)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;"); 
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine("using DBDataLibrary.Attributes;");
            sb.AppendLine("using DBDataLibrary.CRUD;");
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine($"namespace {targetNamespace}");
                sb.AppendLine($"{{");
            }

            sb.AppendLine($"    //  --------------------------------------------------");
            sb.AppendLine($"    // --            AUTOMATIC GENERATED CLASS           --");
            sb.AppendLine($"    // --                DO NOT MODIFY!!!                --");
            sb.AppendLine($"    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --"); 
            sb.AppendLine($"    //  --------------------------------------------------");

            string dataClassName = $"{NormalizeName(table)}";

            sb.AppendLine($"    [TableName(\"{table}\")]");
            sb.AppendLine($"    public partial class {dataClassName} : ACrudBase<{dataClassName}>");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {NormalizeName(table)}() : base() {{ }}");

            sb.AppendLine($"        ");

            var primaryKeys = GetPrimaryKeys(conn, table);

            foreach (var col in columns)
            {
                var type = MapOracleTypeToCSharp(col.DataType, col.Precision, col.Scale, col.IsNullable);
                var propName = NormalizeName(col.Name);
                var fieldName = "_" + char.ToLower(propName[0]) + propName.Substring(1);

                bool isPrimaryKey = primaryKeys.Contains(col.Name);
                bool isRequired = !col.IsNullable && !isPrimaryKey;

                // Field declaration
                //string fieldInitializer = ";";
                ////if (isPrimaryKey || isRequired)
                //{
                //    if (type == "string")
                //        fieldInitializer = " = \"\";";
                //    else if (type == "DateTime")
                //        fieldInitializer = " = DateTime.MinValue;";
                //    else if (!type.EndsWith("?"))
                //        fieldInitializer = $" = default({type});";
                //}
                string? defaultInit = ParseDefaultValue(col.DefaultValue, type);
                string fieldInitializer = defaultInit != null ? $" = {defaultInit};" :
                                             type == "string" ? " = \"\";" :
                                             type == "DateTime" ? " = DateTime.MinValue;" :
                                             !type.EndsWith("?") ? $" = default({type});" : ";";


                sb.AppendLine($"        [NonSerialized] private {type} {fieldName}{fieldInitializer}");

                sb.AppendLine($"        [ColumnName(\"{col.Name}\")]");
                if (isPrimaryKey)
                    sb.AppendLine("        [Key]");

                if (col.IsIdentity)
                    sb.AppendLine("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");

                if (isRequired)
                    sb.AppendLine("        [Required]");

                // Property with notification
                sb.AppendLine($"        public {type} {propName}");
                sb.AppendLine("        {");
                sb.AppendLine($"            get => {fieldName};");
                
                if (!col.IsIdentity)
                {
                    sb.AppendLine("            set");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (!Equals({fieldName}, value))");
                    sb.AppendLine("                {");
                    sb.AppendLine($"                    {fieldName} = value;");
                    sb.AppendLine($"                    AddModifiedProperty(nameof({propName}));");
                    sb.AppendLine("                }");
                    sb.AppendLine("            }");
                }
                sb.AppendLine("        }");
                sb.AppendLine();
            }


            sb.AppendLine("    }");

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        static string GenerateDefineClassCode(string table)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using DBDataLibrary.Attributes;");
            sb.AppendLine("using DBDataLibrary.CRUD;");
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine($"namespace {targetNamespace}");
                sb.AppendLine($"{{");
            }

            sb.AppendLine($"    //  -------------------------------------------");
            sb.AppendLine($"    // --            CUSTOMIZABLE CLASS           --");
            sb.AppendLine($"    // --                   ***                   --");
            sb.AppendLine($"    // --          CHANGES HERE ARE SAFE!         --");
            sb.AppendLine($"    //  -------------------------------------------");

            string dataClassName = $"{NormalizeName(table)}";

            sb.AppendLine($"    // TODO Customize the TableType to allow more functions of the table");
            sb.AppendLine($"    [TableType(TableTypes.ReadOnly)]");
            sb.AppendLine($"    public partial class {dataClassName}");
            sb.AppendLine("    {");
            sb.AppendLine($"         // Keep this clear.");
            sb.AppendLine($"         // Your custom methods should go in the {NormalizeName(table)}.custom.cs class");
            //sb.AppendLine($"         // You will find the file in the Extensions folder");
            sb.AppendLine("    }");

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        static string GenerateExtensionClassCode(string table)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using DBDataLibrary.Attributes;");
            sb.AppendLine("using DBDataLibrary.CRUD;");
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine($"namespace {targetNamespace}");
                sb.AppendLine($"{{");
            }

            sb.AppendLine($"    //  -------------------------------------------");
            sb.AppendLine($"    // --            CUSTOMIZABLE CLASS           --");
            sb.AppendLine($"    // --                   ***                   --");
            sb.AppendLine($"    // --          CHANGES HERE ARE SAFE!         --");
            sb.AppendLine($"    //  -------------------------------------------");

            string dataClassName = $"{NormalizeName(table)}";

            sb.AppendLine($"    public partial class {dataClassName}");
            sb.AppendLine("    {");
            sb.AppendLine("         // Insert your customizations in this class");
            sb.AppendLine("    }");

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        static HashSet<string> GetPrimaryKeys(OracleConnection conn, string tableName)
        {
            var primaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT cols.column_name
                FROM user_constraints cons
                JOIN user_cons_columns cols ON cons.constraint_name = cols.constraint_name
                WHERE cons.constraint_type = 'P' AND cons.table_name = :tableName";

            cmd.Parameters.Add(new OracleParameter("tableName", tableName));

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                primaryKeys.Add(reader.GetString(0));
            }

            return primaryKeys;
        }

    }
}
