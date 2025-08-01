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
    public partial class MfcConvComToMan_data: IDBData
    {
        [ColumnName("OID")]
        [Required]
        public long Oid { get; set; } = default(long);
        [ColumnName("MESSAGE")]
        [Required]
        public string Message { get; set; } = "";
        [ColumnName("DT_INSERT")]
        [Required]
        public string DtInsert { get; set; } = "";
        [ColumnName("LOCAL_ENDPOINT_CODE")]
        public string LocalEndpointCode { get; set; }
        [ColumnName("REMOTE_ENDPOINT_CODE")]
        public string RemoteEndpointCode { get; set; }
        [ColumnName("MESSAGE_TYPE")]
        public string MessageType { get; set; }
        [ColumnName("MESSAGE_CODE")]
        public string MessageCode { get; set; }
    }
}
