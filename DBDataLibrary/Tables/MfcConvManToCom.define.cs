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
    [TableType(TableTypes.Insertable | TableTypes.Deletable)]
    public partial class MfcConvManToCom
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvManToCom.custom.cs class
         
    }
}
