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
    public partial class MfcConvMovZiToSend_data: IDBData
    {
        [ColumnName("OID_UDM")]
        [Required]
        public long OidUdm { get; set; } = default(long);
        [ColumnName("DT_INSERT")]
        [Required]
        public string DtInsert { get; set; } = "";
        [ColumnName("POS_TO")]
        [Required]
        public string PosTo { get; set; } = "";
        [ColumnName("CODE")]
        [Required]
        public string Code { get; set; } = "";
        [ColumnName("ENABLED")]
        public int? Enabled { get; set; }
    }
}
