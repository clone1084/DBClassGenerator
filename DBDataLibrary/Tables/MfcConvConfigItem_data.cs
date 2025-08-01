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
    public partial class MfcConvConfigItem_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
        [ColumnName("CONSTRAIN")]
        [Key]
        public string Constrain { get; set; }
        [ColumnName("TP_ITEM")]
        [Key]
        public int TpItem { get; set; }
        [ColumnName("ACK")]
        public int? Ack { get; set; }
        [ColumnName("OID_AREAS_LOC")]
        public long? OidAreasLoc { get; set; }
    }
}
