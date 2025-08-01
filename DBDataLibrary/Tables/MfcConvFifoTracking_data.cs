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
    public partial class MfcConvFifoTracking_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public int Oid { get; set; }
        [ColumnName("FIFO_TYPE")]
        [Required]
        public string FifoType { get; set; } = "";
        [ColumnName("OID_UDM")]
        [Required]
        public long OidUdm { get; set; } = default(long);
        [ColumnName("DT_INSERT")]
        [Required]
        public string DtInsert { get; set; } = "";
    }
}
