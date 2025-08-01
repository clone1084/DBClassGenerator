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
    public partial class MfcConvProfileCheckError_data: IDBData
    {
        [ColumnName("CD_ERROR")]
        [Required]
        public int CdError { get; set; } = default(int);
        [ColumnName("DSC_ERROR")]
        [Required]
        public string DscError { get; set; } = "";
    }
}
