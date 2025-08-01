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
    public partial class MfcConvEmptyLine_data: IDBData
    {
        [ColumnName("CD_LINE")]
        [Required]
        public int CdLine { get; set; } = default(int);
        [ColumnName("DSC_LINE")]
        public string DscLine { get; set; }
        [ColumnName("TP_UDM")]
        public int? TpUdm { get; set; }
        [ColumnName("ENABLE")]
        public int? Enable { get; set; }
        [ColumnName("AUTO_EXTRACTION_ENABLED")]
        public int? AutoExtractionEnabled { get; set; }
    }
}
