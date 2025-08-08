using log4net;
using System.ComponentModel.Composition;
using System.Data;

namespace DBDataLibrary.CRUD
{
    [InheritedExport]
    public interface ICrudClass
    {        
        string TableName { get; }
        bool Delete(IDbConnection connection, ILog log, string baseLogMessage);
        IEnumerable<string> GetKeys();
        Dictionary<string, object> GetKeyValues();
        bool Insert(IDbConnection connection, ILog log, string baseLogMessage);
        bool IsCached();
        void ReLoadCache(IDbConnection connection, ILog log, string baseLogMessage, CancellationToken cancellationToken);
        bool Update(IDbConnection connection, ILog log, string baseLogMessage);
    }

    [InheritedExport(typeof(ICrudClass<>))]
    public interface ICrudClass<TClass>: ICrudClass
        where TClass : ICrudClass<TClass>, new()
    {
        // GetMany is a static method that retrieves all records of type TClass from the database.
        //IEnumerable<TClass> GetMany(IDbConnection connection, string whereFilter = "");
    }
}