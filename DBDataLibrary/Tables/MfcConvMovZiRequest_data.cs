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
    public partial class MfcConvMovZiRequest_data: IDBData
    {
        [ColumnName("OID")]
        [Required]
        public long Oid { get; set; } = default(long);
        [ColumnName("OID_MFC_CONV_MOVEMENTS")]
        public long? OidMfcConvMovements { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("OLD_POS")]
        public int? OldPos { get; set; }
        [ColumnName("NEW_POS")]
        public int? NewPos { get; set; }
        [ColumnName("DELETE_UDM")]
        public int? DeleteUdm { get; set; }
        [ColumnName("FL_ELABORATE")]
        public int? FlElaborate { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
    }
}
