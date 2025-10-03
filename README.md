# DbDataLibrary

Is the actual working library that must be included in the final project that needs CRUD access to the DBMS tables.

# DbDataClassGenerator

This project will allow the user to **automatically** create CRUD classes starting from an Oracle DataBase table set using DBDataLibrary as a CRUD template.
The DBClass generator console app requirest 3 parameters + 1 optional parameter:

1. -cs <connectionString>: Database connection string like: `"Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MY_SERVICE)));User ID= MY_USER; Password = MyPwd;"`.
2. -ns <namespace>: namespace of the generated classes like: `"MyAssembly.Tables"`.
3. -out <outputDirectory>: directory path for output files like: `C:\MyAssembly\Tables`.
4. *(optional)* -tn <tableNameFilter>: filter to select only some of the DB tables like: `"%MY_TABLE%"`. If not set the application will generate a class for every table.

Usage: `DbDataClassGenerator -cs <connectionString> -ns <namespace> -out <outputDirectory> -tn <tableNameFilter>`

The application will create 3 files for each table:
### 1. **<TABLE_NAME>.table.cs**     
  > [!IMPORTANT]
  > All the generated *.table.cs classes will derive from `DBDataLibrary.CRUD.ACrudBase` to implement all the expected base functions and will have only the columns definition found in the table.
  > 
  > <ins>This file must never be customized, a new execution will overwrite the file and all it's content.</ins>

### 2. **<TABLE_NAME>.custom.cs**
  > [!TIP]
  > All the generated *.custom.cs classes are partial classes and can be customized as needed with methods and properties.
  > 
  > They contain the custom **TableType** tag that allows operation on the table
  >
  > <ins>This file is created only on the first execution and it will not be overwritten.</ins>
  > 
  > <ins>Place in this class your custom methods</ins>
  >  
  
> [!WARNING]
> In case of errors in the use of custom Properites with CRUD operations, mark them as `[NonSerialized]`. The CRUD system is based on reflection and it work only on Properties with the `ColumnNameAttribute` but as we know reflection is not perfect and there may be some mistakes.


## DBDataLibrary.CRUD.ACrudBase
Is the core of this DBMS table access system. It work through reflection and will make use of the generated classes and all the columns and TableType definition.

This is intended to control wich kind of actions are allowed on every table and avoid "possible" mistakes in their common use in the code.

+ **Read**: is always available for every table.
  + Get: will return the first element of a query. This uses LINQ `Expression` to create SQL where clauses. In this way we allow the use of only the expected columns and their proper values.
  + GetMany: will return an IEnumerable<TableClass> with all the results of the query. This has 2 versions:
    1. Uses LINQ `Expression` to create SQL where clauses, this may search directly in the cache if it's available and eventually fall back to the DB;
    2. Uses standard handwritten SQL where clauses and will always query the DBMS.
+ **Insert**: is optional.
  + Must be declared in the `<TABLE_NAME>.custom.cs` file to allow the Insert funcion to be used on the table.
+ **Update**: is optional.
  + Must be declared in the `<TABLE_NAME>.custom.cs` file to allow the Update funcion to be used on the table.
+ **Delete**: is optional.
  + Must be declared in the `<TABLE_NAME>.custom.cs` file to allow the Delete funcion to be used on the table.
+ **Cached**: is optional.
  + May be declared in the `<TABLE_NAME>.custom.cs` file to enable the automatic use of an "in-memory" cache and all the allowed functions will make use of the cache. (Insert, Update and Delete will always commit to the DB too).


# CrudTestApp
If AutomaticTestOfAllClasses method is enebled in its Run() method, this will try to execute all the CRUD actions on the table and return the result to check if the `<TABLE_NAME>.custom.cs` files are decorated as expected. 
*(Still under development)*
