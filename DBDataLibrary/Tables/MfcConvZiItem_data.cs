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
    public partial class MfcConvZiItem_data: IDBData
    {
        [ColumnName("SOURCE_ITEM")]
        public string SourceItem { get; set; }
        [ColumnName("SOURCE_DESCR")]
        public string SourceDescr { get; set; }
        [ColumnName("DEST_ITEM_MIN")]
        public int? DestItemMin { get; set; }
        [ColumnName("DEST_ITEM_MAX")]
        public int? DestItemMax { get; set; }
        [ColumnName("DEST_DESCR")]
        public string DestDescr { get; set; }
        [ColumnName("USE_NERAK")]
        public int? UseNerak { get; set; }
        [ColumnName("ZI_ITEM")]
        public string ZiItem { get; set; }
        [ColumnName("IS_ASCENT")]
        public int? IsAscent { get; set; }
        [ColumnName("USE_ON_MI_0")]
        public int? UseOnMi0 { get; set; }
        [ColumnName("ZI_ITEM_PLC2")]
        public string ZiItemPlc2 { get; set; }
    }
}
