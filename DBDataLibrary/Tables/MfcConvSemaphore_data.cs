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
    public partial class MfcConvSemaphore_data: IDBData
    {
        [ColumnName("LEVEL_ITEM")]
        [Required]
        public int LevelItem { get; set; } = default(int);
        [ColumnName("CD_ITEM")]
        [Required]
        public int CdItem { get; set; } = default(int);
        [ColumnName("STATUS")]
        public int? Status { get; set; }
    }
}
