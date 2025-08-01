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
    public partial class MfcConvMultifunction_data: IDBData
    {
        [ColumnName("CD_UDM")]
        public string CdUdm { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
    }
}
