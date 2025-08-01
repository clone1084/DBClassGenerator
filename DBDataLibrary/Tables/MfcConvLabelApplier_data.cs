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
    public partial class MfcConvLabelApplier_data: IDBData
    {
        [ColumnName("CD_ITEM")]
        public string CdItem { get; set; }
        [ColumnName("FL_STATUS")]
        public int? FlStatus { get; set; }
    }
}
