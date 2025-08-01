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
    public partial class MfcConvDecodeDestination_data: IDBData
    {
        [ColumnName("DEST_TYPE")]
        [Key]
        public int DestType { get; set; }
        [ColumnName("DEST_PAR")]
        [Key]
        public int DestPar { get; set; }
        [ColumnName("CD_ITEM")]
        [Key]
        public string CdItem { get; set; }
    }
}
