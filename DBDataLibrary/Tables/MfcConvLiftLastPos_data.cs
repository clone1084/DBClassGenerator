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
    public partial class MfcConvLiftLastPos_data: IDBData
    {
        [ColumnName("CD_LIFT")]
        [Required]
        public int CdLift { get; set; } = default(int);
        [ColumnName("CD_LAST_ITEM")]
        public string CdLastItem { get; set; }
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
    }
}
