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
    public partial class MfcConvLiftMissions_data: IDBData
    {
        [ColumnName("CD_LIFT")]
        [Required]
        public int CdLift { get; set; } = default(int);
        [ColumnName("TP_MISSION")]
        [Required]
        public int TpMission { get; set; } = default(int);
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("SOURCE")]
        [Required]
        public string Source { get; set; } = "";
        [ColumnName("DEST")]
        [Required]
        public string Dest { get; set; } = "";
        [ColumnName("COUNTER")]
        [Required]
        public int Counter { get; set; } = default(int);
        [ColumnName("RUNNING")]
        [Required]
        public int Running { get; set; } = default(int);
        [ColumnName("ENABLE")]
        [Required]
        public int Enable { get; set; } = default(int);
        [ColumnName("PRIORITY")]
        [Required]
        public int Priority { get; set; } = default(int);
        [ColumnName("ST_MISSION")]
        [Required]
        public int StMission { get; set; } = default(int);
        [ColumnName("LIFT_ITEM")]
        [Required]
        public string LiftItem { get; set; } = "";
        [ColumnName("DT_INSERT_MISSION")]
        [Required]
        public DateTime DtInsertMission { get; set; } = DateTime.MinValue;
        [ColumnName("DT_START_MISSION")]
        public DateTime? DtStartMission { get; set; }
        [ColumnName("DT_UPDATE_MISSION")]
        public DateTime? DtUpdateMission { get; set; }
    }
}
