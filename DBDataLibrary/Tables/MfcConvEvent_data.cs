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
    public partial class MfcConvEvent_data: IDBData
    {
        [ColumnName("OID")]
        public long? Oid { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("OID_TYPE")]
        public int? OidType { get; set; }
        [ColumnName("STATUS")]
        public int? Status { get; set; }
        [ColumnName("DT_INSERT")]
        public string DtInsert { get; set; }
        [ColumnName("DT_UPDATE")]
        public string DtUpdate { get; set; }
        [ColumnName("INSERTED_BY")]
        public string InsertedBy { get; set; }
    }
}
