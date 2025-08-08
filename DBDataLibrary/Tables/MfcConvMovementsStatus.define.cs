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
    // Customize the TableType to allow more functions of the table
    [TableType(TableTypes.Updatable | TableTypes.Cached)]
    public partial class MfcConvMovementsStatus
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvMovementsStatus.custom.cs class
         
    }
}
