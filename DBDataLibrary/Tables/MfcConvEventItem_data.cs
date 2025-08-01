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
    public partial class MfcConvEventItem_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("OID_TYPE")]
        [Required]
        public long OidType { get; set; } = default(long);
    }
}
