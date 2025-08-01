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
    public partial class MfcConvGroupStatusExt_data: IDBData
    {
        [ColumnName("CD_GROUP")]
        [Key]
        public string CdGroup { get; set; }
        [ColumnName("CD_GROUPING")]
        [Required]
        public int CdGrouping { get; set; } = default(int);
        [ColumnName("PRIORITY")]
        public int? Priority { get; set; }
    }
}
