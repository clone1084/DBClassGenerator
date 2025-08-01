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
    public partial class MfcConvLifts_data: IDBData
    {
        [ColumnName("LIFT")]
        [Required]
        public string Lift { get; set; } = "";
        [ColumnName("DSC")]
        public string Dsc { get; set; }
        [ColumnName("AVAILABLE")]
        [Required]
        public int Available { get; set; } = default(int);
        [ColumnName("ZI")]
        [Required]
        public string Zi { get; set; } = "";
        [ColumnName("COUNT")]
        [Required]
        public int Count { get; set; } = default(int);
        [ColumnName("MAX_COUNT")]
        public int? MaxCount { get; set; }
    }
}
