using System;
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
    // TODO Customize the TableType to allow more functions of the table
    [TableType(TableTypes.ReadOnly | TableTypes.Cached)]
    public partial class MfcConvAutomaticMovements
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvAutomaticMovements.custom.cs class
         
    }
}
