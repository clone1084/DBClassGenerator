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
    public partial class MfcConvZtToSend_data: IDBData
    {
        [ColumnName("OID_UDM")]
        [Required]
        public long OidUdm { get; set; } = default(long);
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("DT_INSERT")]
        public string DtInsert { get; set; }
    }
}
