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
    [TableType(TableTypes.Updatable)]
    public partial class MfcConvEmptyLineDetails
    {
         // Keep this clear.
         // Your custom methods should go in the MfcConvEmptyLineDetails.custom.cs class
         
    }
}
