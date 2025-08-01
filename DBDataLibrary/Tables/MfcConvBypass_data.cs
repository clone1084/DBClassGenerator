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
    public partial class MfcConvBypass_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("CD_GROUP")]
        public string CdGroup { get; set; }
        [ColumnName("CD_ITEM_NEXT_1")]
        public string CdItemNext1 { get; set; }
        [ColumnName("CD_ITEM_NEXT_2")]
        public string CdItemNext2 { get; set; }
        [ColumnName("CD_ITEM_NEXT_3")]
        public string CdItemNext3 { get; set; }
        [ColumnName("CD_ITEM_PREV_1")]
        public string CdItemPrev1 { get; set; }
        [ColumnName("CD_ITEM_PREV_2")]
        public string CdItemPrev2 { get; set; }
        [ColumnName("CD_TYPE")]
        public int? CdType { get; set; }
    }
}
