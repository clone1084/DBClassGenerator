using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.Attributes
{
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
        Undefined = 0<<0,        //0
        /// <summary>
        /// This table is static, meaning it does not change and is not expected to be modified.
        /// It is typically used for reference data or configuration settings.
        /// </summary>
        Static = 1<<0,      //1
        Insertable = 1<<1,  //2
        Updatable = 1<<2,   //4
        Deletable = 1<<3,   //8
    }
}
