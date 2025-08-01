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
    public partial class MfcConvGroupStatus_data: IDBData
    {
        [ColumnName("CD_GROUP")]
        [Key]
        public string CdGroup { get; set; }
        [ColumnName("CD_ST_GROUP")]
        public int? CdStGroup { get; set; }
        [ColumnName("CD_CONVEYOR")]
        [Key]
        public int CdConveyor { get; set; }
        [ColumnName("FL_AUTO_WARMSTART")]
        public int? FlAutoWarmstart { get; set; }
        [ColumnName("CD_CODE")]
        public int? CdCode { get; set; }
    }
}
