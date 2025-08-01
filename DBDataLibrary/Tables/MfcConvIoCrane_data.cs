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
    public partial class MfcConvIoCrane_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("DIRECTION")]
        [Required]
        public int Direction { get; set; } = default(int);
        [ColumnName("CD_CRANE")]
        [Required]
        public int CdCrane { get; set; } = default(int);
        [ColumnName("CD_POS_HEAD")]
        [Required]
        public int CdPosHead { get; set; } = default(int);
        [ColumnName("COORDINATE_X")]
        public int? CoordinateX { get; set; }
        [ColumnName("COORDINATE_Y")]
        public int? CoordinateY { get; set; }
        [ColumnName("COORDINATE_Z")]
        public int? CoordinateZ { get; set; }
        [ColumnName("CD_ITEM_REJECT")]
        [Required]
        public string CdItemReject { get; set; } = "";
        [ColumnName("CD_ITEM_ZI")]
        public string CdItemZi { get; set; }
        [ColumnName("PRIORITY")]
        public int? Priority { get; set; }
        [ColumnName("CONSTRAINT")]
        [Required]
        public string Constraint { get; set; } = "";
        [ColumnName("CD_ITEM_UNLOAD")]
        public string CdItemUnload { get; set; }
        [ColumnName("CD_ITEM_LOAD")]
        public string CdItemLoad { get; set; }
        [ColumnName("FLOOR")]
        [Required]
        public int Floor { get; set; } = default(int);
    }
}
