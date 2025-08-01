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
    public partial class MfcConvMovements_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("START_TYPE")]
        public int? StartType { get; set; }
        [ColumnName("START_PAR1")]
        public int? StartPar1 { get; set; }
        [ColumnName("START_PAR2")]
        public long? StartPar2 { get; set; }
        [ColumnName("START_PAR3")]
        public int? StartPar3 { get; set; }
        [ColumnName("START_PAR4")]
        public int? StartPar4 { get; set; }
        [ColumnName("DEST_TYPE")]
        public int? DestType { get; set; }
        [ColumnName("DEST_PAR1")]
        public int? DestPar1 { get; set; }
        [ColumnName("DEST_PAR2")]
        public int? DestPar2 { get; set; }
        [ColumnName("DEST_PAR3")]
        public int? DestPar3 { get; set; }
        [ColumnName("DEST_PAR4")]
        public int? DestPar4 { get; set; }
        [ColumnName("ACTUAL_TYPE")]
        public int? ActualType { get; set; }
        [ColumnName("ACTUAL_PAR1")]
        public int? ActualPar1 { get; set; }
        [ColumnName("ACTUAL_PAR2")]
        public int? ActualPar2 { get; set; }
        [ColumnName("ACTUAL_PAR3")]
        public int? ActualPar3 { get; set; }
        [ColumnName("ACTUAL_PAR4")]
        public int? ActualPar4 { get; set; }
        [ColumnName("PRIORITY")]
        public int? Priority { get; set; }
        [ColumnName("FINISHED")]
        public int? Finished { get; set; }
        [ColumnName("RESULT")]
        public int? Result { get; set; }
        [ColumnName("CONSTRAINT")]
        public string Constraint { get; set; }
        [ColumnName("ROUND_BACK")]
        public int? RoundBack { get; set; }
        [ColumnName("DT_START")]
        public DateTime? DtStart { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("FL_CHECK_PICKING")]
        public int? FlCheckPicking { get; set; }
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
        [ColumnName("LAST_ZI_ITEM")]
        public string LastZiItem { get; set; }
        [ColumnName("FL_MOVING")]
        [Required]
        public int FlMoving { get; set; } = default(int);
    }
}
