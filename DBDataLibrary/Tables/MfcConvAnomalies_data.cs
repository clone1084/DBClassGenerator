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
    public partial class MfcConvAnomalies_data: IDBData
    {
        [ColumnName("CD_ZONE")]
        [Required]
        public string CdZone { get; set; } = "";
        [ColumnName("DT_START_ANOMALY")]
        [Required]
        public DateTime DtStartAnomaly { get; set; } = DateTime.MinValue;
        [ColumnName("DT_END_ANOMALY")]
        public DateTime? DtEndAnomaly { get; set; }
        [ColumnName("CD_ERROR")]
        [Required]
        public int CdError { get; set; } = default(int);
        [ColumnName("TP_ERROR")]
        public int? TpError { get; set; }
    }
}
