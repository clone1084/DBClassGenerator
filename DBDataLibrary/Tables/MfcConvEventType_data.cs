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
    public partial class MfcConvEventType_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("DSC_TYPE")]
        [Required]
        public string DscType { get; set; } = "";
        [ColumnName("EVENT_GROUP")]
        public int? EventGroup { get; set; }
        [ColumnName("DESC_COMMENT")]
        public string DescComment { get; set; }
    }
}
