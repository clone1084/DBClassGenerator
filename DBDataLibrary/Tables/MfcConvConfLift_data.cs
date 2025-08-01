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
    public partial class MfcConvConfLift_data: IDBData
    {
        [ColumnName("CD_LIFT")]
        [Required]
        public int CdLift { get; set; } = default(int);
        [ColumnName("CD_ITEM1")]
        [Required]
        public string CdItem1 { get; set; } = "";
        [ColumnName("CD_ITEM2")]
        public string CdItem2 { get; set; }
        [ColumnName("CONSTRAIN")]
        public string Constrain { get; set; }
        [ColumnName("ST_ENABLED")]
        public int? StEnabled { get; set; }
        [ColumnName("CD_ITEM_REJECT")]
        public string CdItemReject { get; set; }
        [ColumnName("CD_ITEM_UNLOAD")]
        public string CdItemUnload { get; set; }
        [ColumnName("CD_ITEM_DEFAULT_POS")]
        public string CdItemDefaultPos { get; set; }
        [ColumnName("CHECK_UNLOAD_ITEM")]
        public int? CheckUnloadItem { get; set; }
        [ColumnName("RETURN_DEFAULT_POSITION")]
        public int? ReturnDefaultPosition { get; set; }
    }
}
