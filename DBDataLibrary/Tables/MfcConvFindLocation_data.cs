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
    public partial class MfcConvFindLocation_data: IDBData
    {
        [ColumnName("OID")]
        public int? Oid { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("CD_CRANE")]
        public int? CdCrane { get; set; }
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("TP_ITEM")]
        public int? TpItem { get; set; }
        [ColumnName("OID_AREAS")]
        public long? OidAreas { get; set; }
        [ColumnName("ST_REQUEST")]
        public int? StRequest { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
    }
}
