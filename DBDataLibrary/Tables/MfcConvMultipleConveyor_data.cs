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
    public partial class MfcConvMultipleConveyor_data: IDBData
    {
        [ColumnName("ITEM_1")]
        [Required]
        public string Item1 { get; set; } = "";
        [ColumnName("ITEM_2")]
        [Required]
        public string Item2 { get; set; } = "";
        [ColumnName("ITEM_3")]
        public string Item3 { get; set; }
        [ColumnName("ITEM_4")]
        public string Item4 { get; set; }
        [ColumnName("ITEM_5")]
        public string Item5 { get; set; }
        [ColumnName("ITEM_6")]
        public string Item6 { get; set; }
        [ColumnName("ITEM_7")]
        public string Item7 { get; set; }
        [ColumnName("ITEM_8")]
        public string Item8 { get; set; }
        [ColumnName("ITEM_DEST")]
        [Required]
        public string ItemDest { get; set; } = "";
        [ColumnName("ITEM_PRE1")]
        public string ItemPre1 { get; set; }
        [ColumnName("ITEM_PRE2")]
        public string ItemPre2 { get; set; }
        [ColumnName("ITEM_PRE3")]
        public string ItemPre3 { get; set; }
        [ColumnName("ITEM_PRE4")]
        public string ItemPre4 { get; set; }
        [ColumnName("ITEM_PRE5")]
        public string ItemPre5 { get; set; }
        [ColumnName("ITEM_PRE6")]
        public string ItemPre6 { get; set; }
        [ColumnName("TIMEOUT")]
        public int? Timeout { get; set; }
    }
}
