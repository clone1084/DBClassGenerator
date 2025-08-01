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
    public partial class MfcConvItemToReject_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem { get; set; } = "";
        [ColumnName("CONSTRAIN")]
        [Required]
        public string Constrain { get; set; } = "";
        [ColumnName("CD_REJECT_ITEM")]
        [Required]
        public string CdRejectItem { get; set; } = "";
    }
}
