using System.Data;

namespace DBDataLibrary.CRUD
{
    public interface ICrudClass
    {        
        string TableName { get; }
        bool Delete(IDbConnection connection);
        Dictionary<string, object> GetKeyValues();
        bool Insert(IDbConnection connection);
        bool Update(IDbConnection connection);
    }

    public interface ICrudClass<TClass>: ICrudClass
        where TClass : ICrudClass<TClass>, new()
    {
        // LoadAll is a static method that retrieves all records of type TClass from the database.
        //IEnumerable<TClass> LoadAll(IDbConnection connection, string whereFilter = "");
    }
}