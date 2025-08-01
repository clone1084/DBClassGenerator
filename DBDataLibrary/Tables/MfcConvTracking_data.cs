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
    public partial class MfcConvTracking_data: IDBData
    {
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
        [ColumnName("CD_GROUP")]
        public string CdGroup { get; set; }
        [ColumnName("POS_OCCUPIED")]
        [Required]
        public int PosOccupied { get; set; } = default(int);
        [ColumnName("A_MESSAGE")]
        [Required]
        public int AMessage { get; set; } = default(int);
        [ColumnName("DT_UPDATE")]
        public string DtUpdate { get; set; }
    }
}
