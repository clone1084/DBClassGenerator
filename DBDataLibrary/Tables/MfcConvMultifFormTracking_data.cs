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
    public partial class MfcConvMultifFormTracking_data: IDBData
    {
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
    }
}
