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
    public partial class MfcConvPickToLight_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("OID_UDC")]
        public long? OidUdc { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("ST_ELABORATED")]
        public int? StElaborated { get; set; }
    }
}
