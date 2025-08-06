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
    [TableType(TableTypes.Insertable | TableTypes.Updatable | TableTypes.Deletable)]
    public partial class MfcConvMovements
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvMovements.custom.cs class
         
    }
}
