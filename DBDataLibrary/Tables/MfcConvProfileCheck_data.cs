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
    public partial class MfcConvProfileCheck_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
        [ColumnName("CD_FIELD")]
        [Key]
        public string CdField { get; set; }
        [ColumnName("VALUE")]
        public string Value { get; set; }
    }
}
