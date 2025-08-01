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
    public partial class MfcConvInputItems_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
        [ColumnName("TP_ITEM")]
        public int? TpItem { get; set; }
        [ColumnName("FL_EXIT_LINE")]
        public int? FlExitLine { get; set; }
        [ColumnName("ST_CONFIG_MODE")]
        public int? StConfigMode { get; set; }
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate { get; set; }
        [ColumnName("CD_ITEM_DEST")]
        public string CdItemDest { get; set; }
        [ColumnName("FL_TO_STREACH")]
        public int? FlToStreach { get; set; }
    }
}
