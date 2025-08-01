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
    public partial class MfcConvGroupColdstart_data: IDBData
    {
        [ColumnName("CD_GROUP")]
        [Required]
        public string CdGroup { get; set; } = "";
        [ColumnName("CD_STATION")]
        [Required]
        public string CdStation { get; set; } = "";
    }
}
