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
    public partial class MfcConvSendToMultifunction_data: IDBData
    {
        [ColumnName("OID_UDC")]
        public long? OidUdc { get; set; }
        [ColumnName("COD_SCARTO")]
        public int? CodScarto { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("DESCR_SCARTO")]
        public string DescrScarto { get; set; }
        [ColumnName("FL_DELIVERED")]
        public int? FlDelivered { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("ID_SCARTO")]
        public int? IdScarto { get; set; }
    }
}
