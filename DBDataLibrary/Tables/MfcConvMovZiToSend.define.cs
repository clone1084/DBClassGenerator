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
    [TableType(TableTypes.CRUD)]
    public partial class MfcConvMovZiToSend
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvMovZiToSend.custom.cs class
         
    }
}
