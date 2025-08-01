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
    public partial class MfcConvItemSynchro_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
        [ColumnName("FL_PLT_READY")]
        public int? FlPltReady { get; set; }
        [ColumnName("OID_UDM")]
        public int? OidUdm { get; set; }
        [ColumnName("DT_UPDATE")]
        [Required]
        public string DtUpdate { get; set; } = "";
    }
}
