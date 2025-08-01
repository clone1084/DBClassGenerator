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
    public partial class MfcConvMovementsErrors_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("OID_MOVEMENT")]
        [Required]
        public long OidMovement { get; set; } = default(long);
        [ColumnName("RESULT")]
        public long? Result { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
    }
}
