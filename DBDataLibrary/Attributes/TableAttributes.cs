using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; }
        public TableNameAttribute(string tableName) => TableName = tableName;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; }
        public ColumnNameAttribute(string name) => Name = name;
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableTypeAttribute : Attribute
    {
        public TableTypes TableType { get; }
        public TableTypeAttribute(TableTypes tableType) => TableType = tableType;
    }

    [Flags]
    public enum TableTypes
    {
        /// <summary>
        /// 0. This is the default value, indicating that no specific type has been set for the table.
        /// </summary>
        Undefined = 0 << 0,   //0
        /// <summary>
        /// 1. Thi table is read-only, meaning it cannot be modified. None of the CRUD operations (Insert, Update, Delete) are allowed.
        /// </summary>
        ReadOnly = 1 << 0,    //1
        /// <summary>
        /// 2. Insert is allowed on this table.
        /// </summary>
        Insertable = 1 << 1,  //2
        /// <summary>
        /// 4. Update is allowed on this table.
        /// </summary>
        Updatable = 1 << 2,   //4
        /// <summary>
        /// 8. Delete is allowed on this table.
        /// </summary>
        Deletable = 1 << 3,   //8
        /// <summary>
        /// 16. This table is cached, meaning its data is stored in memory for faster access. This impact Load and LoadAll methods.
        /// <para>May be used in conjunction with ReadOnly to indicate that the cached data should not be modified.</para>
        /// <para>May also be used with Insertable, Updatable, and Deletable to indicate that the cached data can be modified. This will cause the cache to be updated accordingly.</para>
        /// </summary>
        Cached = 1 << 4,      //16
    }
}
