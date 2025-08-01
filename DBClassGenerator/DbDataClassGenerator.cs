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
        static bool generateCrudClasses = false;

        public static void GenerateClasses(string[] args)
        {
            ParseArgs(args);

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

                var columns = GetColumns(conn, tableName);
                var classCode = GenerateClassCode(tableName, columns, conn);

                File.WriteAllText(Path.Combine(outputDirectory, $"{NormalizeName(tableName)}_data.cs"), classCode);
                Console.WriteLine($"    Generated: {NormalizeName(tableName)}_data.cs");

                if (generateCrudClasses)
                {
                    var crudCode = GenerateCrudClassCode(tableName);
                    File.WriteAllText(Path.Combine(outputDirectory, $"{NormalizeName(tableName)}.cs"), crudCode);
                    Console.WriteLine($"    Generated: {NormalizeName(tableName)}.cs");
                }
            }

            Console.WriteLine($"---");
            Console.WriteLine($"Generated {tableCount} classes in {DateTime.Now.Subtract(startTime).TotalSeconds:N3} s");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-cs") connectionString = args[i + 1];
                if (args[i] == "-ns") targetNamespace = args[i + 1];
                if (args[i] == "-out") outputDirectory = args[i + 1];
                if (args[i] == "-tn") tableNameFilter = args[i + 1];
                if (args[i] == "-gc") generateCrudClasses = args[i + 1].Trim().ToUpper() == "Y";
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Console.WriteLine($"ConnectionString: {connectionString}");
            Console.WriteLine($"TableName(s): {tableNameFilter}");
            Console.WriteLine($"NameSpace: {targetNamespace}");
            Console.WriteLine($"OutputDirectory: {outputDirectory}");
            Console.WriteLine($"Generate CRUD classes: {generateCrudClasses}");
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
        }

        static List<ColumnInfo> GetColumns(OracleConnection conn, string tableName)
        {
            var list = new List<ColumnInfo>();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT column_name, data_type, nullable, data_precision, data_scale
                                FROM user_tab_columns 
                                WHERE table_name = :tableName ORDER BY column_id";
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
                });
            }
            return list;
        }

        static string MapOracleTypeToCSharp(string oracleType, int? precision, int? scale, bool isNullable)
        {
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
            sb.AppendLine("using DBDataLibrary.Attributes;");
            sb.AppendLine("using DBDataLibrary.DataTypes;");
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

            string dataClassName = $"{NormalizeName(table)}_data";

            //sb.AppendLine($"    public partial class {table}_data : ADBData");
            sb.AppendLine($"    public partial class {dataClassName}: ADBData");
            sb.AppendLine("    {");

            var primaryKeys = GetPrimaryKeys(conn, table);

            foreach (var col in columns)
            {
                var type = MapOracleTypeToCSharp(col.DataType, col.Precision, col.Scale, col.IsNullable);
                var prop = NormalizeName(col.Name);

                sb.AppendLine($"        [ColumnName(\"{col.Name}\")]");

                bool isPrimaryKey = primaryKeys.Contains(col.Name);
                bool isRequired = !col.IsNullable && !isPrimaryKey;

                if (isPrimaryKey)
                    sb.AppendLine("        [Key]");

                if (isRequired)
                    sb.AppendLine("        [Required]");

                // inizializzazione: solo se Required
                string initializer = "";
                if (isRequired)
                {
                    if (type == "string")
                        initializer = " = \"\";";
                    else if (type == "DateTime")
                        initializer = " = DateTime.MinValue;";
                    else if (!type.EndsWith("?"))
                        initializer = $" = default({type});";

                }

                sb.AppendLine($"        public {type} {prop} {{ get; set; }}{initializer}");
            }

            sb.AppendLine("    }");

            if (!string.IsNullOrWhiteSpace(targetNamespace))
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        static string GenerateCrudClassCode(string table)
        {
            var sb = new StringBuilder(); 
            sb.AppendLine("using DBDataLibrary.Attributes;");
            sb.AppendLine("using DBDataLibrary.CRUD;");

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

            string crudClassName = NormalizeName(table);
            string dataClassName = $"{crudClassName}_data";

            sb.AppendLine("    [TableType(TableTypes.Undefined)]");
            sb.AppendLine($"    public partial class {crudClassName} : ACrudBase<{crudClassName}, {dataClassName}>");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {NormalizeName(table)}() : base() {{ }}");
            sb.AppendLine($"        public override string TableName => \"{table}\";"); 
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
