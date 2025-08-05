using log4net;
using System.Data;

namespace DBDataLibrary.CRUD
{
    public interface ICrudClass
    {        
        string TableName { get; }
        bool Delete(IDbConnection connection, ILog log, string baseLogMessage);
        IEnumerable<string> GetKeys();
        Dictionary<string, object> GetKeyValues();
        bool Insert(IDbConnection connection, ILog log, string baseLogMessage);
        bool Update(IDbConnection connection, ILog log, string baseLogMessage);
    }

    public interface ICrudClass<TClass>: ICrudClass
        where TClass : ICrudClass<TClass>, new()
    {
        // LoadAll is a static method that retrieves all records of type TClass from the database.
        //IEnumerable<TClass> LoadAll(IDbConnection connection, string whereFilter = "");
    }
}