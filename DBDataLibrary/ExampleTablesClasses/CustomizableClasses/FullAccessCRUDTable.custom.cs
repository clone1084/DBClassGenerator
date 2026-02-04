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
    [TableType(TableTypes.CRUD)]
    public partial class FullAccessCRUDTableable
    {
         // Insert your customizations in this class
         
         // Example: You can override GetReadOnlyOnUpdateProperties() to prevent
         // certain properties from being updated, even though the table allows CRUD operations
         // protected override HashSet<string> GetReadOnlyOnUpdateProperties()
         // {
         //     return new HashSet<string> { nameof(SomeProperty) };
         // }
    }
}
