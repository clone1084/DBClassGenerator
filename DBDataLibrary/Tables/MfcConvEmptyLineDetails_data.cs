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
    public partial class MfcConvEmptyLineDetails_data: IDBData
    {
        [ColumnName("CD_LINE")]
        [Required]
        public int CdLine { get; set; } = default(int);
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("POS_STATUS")]
        public int? PosStatus { get; set; }
        [ColumnName("FULL")]
        public int? Full { get; set; }
        [ColumnName("TYPE_MSG")]
        public string TypeMsg { get; set; }
    }
}
