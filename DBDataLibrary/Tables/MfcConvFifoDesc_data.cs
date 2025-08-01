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
    public partial class MfcConvFifoDesc_data: IDBData
    {
        [ColumnName("CD_FIFO")]
        [Required]
        public int CdFifo { get; set; } = default(int);
        [ColumnName("CD_ITEM_HEAD")]
        [Required]
        public string CdItemHead { get; set; } = "";
        [ColumnName("CD_ITEM_TAIL")]
        [Required]
        public string CdItemTail { get; set; } = "";
        [ColumnName("NUM_UDM")]
        public int? NumUdm { get; set; }
    }
}
