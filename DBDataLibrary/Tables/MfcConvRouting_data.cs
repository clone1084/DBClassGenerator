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
    public partial class MfcConvRouting_data: IDBData
    {
        [ColumnName("CD_ITEM_FROM")]
        [Key]
        public string CdItemFrom { get; set; }
        [ColumnName("CD_ITEM_TO")]
        [Key]
        public string CdItemTo { get; set; }
        [ColumnName("CD_ITEM_NEXT")]
        [Required]
        public string CdItemNext { get; set; } = "";
        [ColumnName("ACK")]
        [Required]
        public int Ack { get; set; } = default(int);
    }
}
