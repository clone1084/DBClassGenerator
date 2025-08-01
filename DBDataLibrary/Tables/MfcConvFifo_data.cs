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
    public partial class MfcConvFifo_data: IDBData
    {
        [ColumnName("CD_FIFO")]
        public int? CdFifo { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("OID_UDM")]
        [Required]
        public long OidUdm { get; set; } = default(long);
        [ColumnName("CD_UDM")]
        public string CdUdm { get; set; }
        [ColumnName("ID_ZONE")]
        [Required]
        public string IdZone { get; set; } = "";
        [ColumnName("CD_UDS")]
        public string CdUds { get; set; }
        [ColumnName("ST_UDS")]
        [Required]
        public int StUds { get; set; } = default(int);
        [ColumnName("PLC_INC")]
        [Required]
        public int PlcInc { get; set; } = default(int);
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
        [ColumnName("PLC_BARCODE")]
        public string PlcBarcode { get; set; }
        [ColumnName("PLC_SCANNER")]
        public string PlcScanner { get; set; }
        [ColumnName("CD_ERROR")]
        public int? CdError { get; set; }
    }
}
