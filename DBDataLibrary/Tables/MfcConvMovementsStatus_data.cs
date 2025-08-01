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
    public partial class MfcConvMovementsStatus_data: IDBData
    {
        [ColumnName("CD_INDEX")]
        [Key]
        public string CdIndex { get; set; }
        [ColumnName("FROM_POS")]
        [Required]
        public string FromPos { get; set; } = "";
        [ColumnName("TO_POS")]
        [Required]
        public string ToPos { get; set; } = "";
        [ColumnName("ST_MOV")]
        public int? StMov { get; set; }
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
        [ColumnName("TIMEOUT")]
        public int? Timeout { get; set; }
    }
}
