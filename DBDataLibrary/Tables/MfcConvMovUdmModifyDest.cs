using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;

namespace DBDataLibrary.Tables
{
    //  --------------------------------------------------
    // --            AUTOMATIC GENERATED CLASS           --
    // --                DO NOT MODIFY!!!                --
    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --
    //  --------------------------------------------------
    [TableName("MFC_CONV_MOV_UDM_MODIFY_DEST")]
    public partial class MfcConvMovUdmModifyDest : ACrudBase<MfcConvMovUdmModifyDest>
    {
        public MfcConvMovUdmModifyDest() : base() { }
        
        [NonSerialized] private int? _pos;
        [ColumnName("POS")]
        public int? Pos
        {
            get => _pos;
            set
            {
                if (!Equals(_pos, value))
                {
                    _pos = value;
                    AddModifiedProperty(nameof(Pos));
                }
            }
        }

    }
}
