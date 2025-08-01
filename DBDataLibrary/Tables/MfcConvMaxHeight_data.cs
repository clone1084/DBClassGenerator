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
    public partial class MfcConvMaxHeight_data: IDBData
    {
        [ColumnName("ITEM_START")]
        public string ItemStart { get; set; }
        [ColumnName("ITEM_DEST")]
        public string ItemDest { get; set; }
        [ColumnName("CD_PARAMETER")]
        public string CdParameter { get; set; }
    }
}
