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
    public partial class MfcConvRoutingPlc_data: IDBData
    {
        [ColumnName("CD_ITEM_TO")]
        [Required]
        public string CdItemTo { get; set; } = "";
        [ColumnName("CD_ITEM_NEXT")]
        [Required]
        public string CdItemNext { get; set; } = "";
    }
}
