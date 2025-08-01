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
    public partial class MfcConvUdmToMove_data: IDBData
    {
        [ColumnName("CD_UDC")]
        [Required]
        public string CdUdc { get; set; } = "";
        [ColumnName("CD_UDM")]
        [Required]
        public string CdUdm { get; set; } = "";
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("INSERT_BY")]
        public string InsertBy { get; set; }
        [ColumnName("FILE_SOURCE")]
        public string FileSource { get; set; }
    }
}
