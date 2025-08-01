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
    public partial class MfcConvEventConfigThread_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("NAME")]
        [Required]
        public string Name { get; set; } = "";
        [ColumnName("PRIORITY_FROM")]
        public int? PriorityFrom { get; set; }
        [ColumnName("PRIORITY_TO")]
        public int? PriorityTo { get; set; }
        [ColumnName("PLC")]
        [Required]
        public int Plc { get; set; } = default(int);
        [ColumnName("TIMER")]
        [Required]
        public int Timer { get; set; } = default(int);
        [ColumnName("EVENT_GROUP")]
        public int? EventGroup { get; set; }
        [ColumnName("PROCESS_NAME")]
        [Required]
        public string ProcessName { get; set; } = "";
    }
}
