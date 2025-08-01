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
    public partial class MfcConvSpecialCross_data: IDBData
    {
        [ColumnName("CD_CROSS")]
        [Required]
        public string CdCross { get; set; } = "";
        [ColumnName("FIRST")]
        [Required]
        public string First { get; set; } = "";
        [ColumnName("SECOND")]
        [Required]
        public string Second { get; set; } = "";
        [ColumnName("TOT")]
        public int? Tot { get; set; }
        [ColumnName("COUNTER")]
        public int? Counter { get; set; }
    }
}
