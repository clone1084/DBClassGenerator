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
        Undefined = 0 << 0,        //0
        /// <summary>
        /// Thi table is read-only, meaning it cannot be modified.
        /// </summary>
        ReadOnly = 1 << 0,      //1
        Insertable = 1 << 1,  //2
        Updatable = 1 << 2,   //4
        Deletable = 1 << 3,   //8
        /// <summary>
        /// This table is cached, meaning its data is stored in memory for faster access.
        /// </summary>
        Cached = 1 << 4,    //16
    }
}
