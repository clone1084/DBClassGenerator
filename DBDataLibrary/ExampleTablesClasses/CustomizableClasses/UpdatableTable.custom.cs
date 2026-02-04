using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;

namespace DBDataLibrary.Tables
{
    //  -------------------------------------------
    // --            CUSTOMIZABLE CLASS           --
    // --                   ***                   --
    // --          CHANGES HERE ARE SAFE!         --
    //  -------------------------------------------
    // Customize the TableType to allow more functions of the table
    [TableType(TableTypes.Updatable)]
    public partial class UpdatableTable
    {
        // Insert your customizations in this class
        
        /// <summary>
        /// Override this method to specify which properties cannot be updated.
        /// Properties listed here will still track changes (via setter), 
        /// but will be excluded from UPDATE SQL statements.
        /// This is useful for properties that should only be set during INSERT
        /// or that represent business keys that should never change.
        /// </summary>
        protected override HashSet<string> GetReadOnlyOnUpdateProperties()
        {
            return new HashSet<string>
            {
                nameof(CdItem),                // CdItem cannot be updated (business key)
                nameof(Constrain),             // Constrain cannot be updated
                nameof(ReturnDefaultPosition), // ReturnDefaultPosition cannot be updated
            };
        }
    }
}
