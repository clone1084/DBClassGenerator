using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableNameAttribute(string tableName) : Attribute
    {
        public string TableName { get; } = tableName;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnNameAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableTypeAttribute(TableTypes tableType) : Attribute
    {
        public TableTypes TableType { get; } = tableType;
    }

    [Flags]
    /// <summary>   
    /// Represents the types of operations that can be performed on a table.
    /// <para>Read is always allowed</para>
    /// </summary>
    public enum TableTypes : byte
    {
        ///// <summary>
        ///// 0. This allows all operations on the table, including Insert, Update, Delete, and Read. 
        ///// <para>Handle with care!</para>
        ///// </summary>
        //None        = 0,      // Nessun flag attivo
        
        /// <summary>
        /// 1. Thi table is read-only, meaning it cannot be modified. 
        /// <para>None of the other operations (Insert, Update, Delete) are allowed.</para>
        /// </summary>
        ReadOnly = 1 << 0, // 1

        /// <summary>
        /// 2. Insert is allowed on this table.
        /// </summary>
        Insertable = 1 << 1, // 2
        /// <summary>
        /// 4. Update is allowed on this table.
        /// </summary>
        Updatable  = 1 << 2, // 4
        /// <summary>
        /// 8. Delete is allowed on this table.
        /// </summary>
        Deletable  =  1 << 3, // 8

        /// <summary>
        /// 14. This table allows all CRUD operations: Insert, Update, and Delete.
        /// </summary>
        CRUD = Insertable | Updatable | Deletable, // 14

        /// <summary>
        /// 128. This table is cached, meaning its data is stored in memory for faster access. This impact Get and GetMany methods.
        /// <para>May be used in conjunction with ReadOnly to indicate that the cached data should not be modified.</para>
        /// <para>May also be used with Insertable, Updatable, and Deletable to indicate that the cached data can be modified. 
        /// This will cause the cache to be updated accordingly.</para>
        /// </summary>
        Cached = 1 << 7, // 128 (maximum value is 128)
    }
}
