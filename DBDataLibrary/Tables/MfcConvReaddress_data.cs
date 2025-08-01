using System;
using System.ComponentModel.DataAnnotations;
using DBDataLibrary.Attributes;
using DBDataLibrary.DataTypes;

namespace DBDataLibrary.Tables
{
    //  --------------------------------------------------
    // --            AUTOMATIC GENERATED CLASS           --
    // --                DO NOT MODIFY!!!                --
    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --
    //  --------------------------------------------------
    public partial class MfcConvReaddress_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("CD_DESTINATION")]
        [Required]
        public string CdDestination { get; set; } = "";
    }
}
