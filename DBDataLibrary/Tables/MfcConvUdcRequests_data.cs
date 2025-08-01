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
    public partial class MfcConvUdcRequests_data: IDBData
    {
        [ColumnName("BARCODE")]
        public string Barcode { get; set; }
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("DT_INSERT")]
        [Required]
        public string DtInsert { get; set; } = "";
        [ColumnName("ST_ELAB")]
        [Required]
        public int StElab { get; set; } = default(int);
        [ColumnName("GUID")]
        [Key]
        public string Guid { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
    }
}
