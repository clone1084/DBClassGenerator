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
    [TableName("MFC_CONV_IO_LIFT")]
    public partial class MfcConvIoLift : ACrudBase<MfcConvIoLift>
    {
        public MfcConvIoLift() : base() { }
        
        [NonSerialized] private int _cdLift = default(int);
        [ColumnName("CD_LIFT")]
        [Required]
        public int CdLift
        {
            get => _cdLift;
            set
            {
                if (!Equals(_cdLift, value))
                {
                    _cdLift = value;
                    AddModifiedProperty(nameof(CdLift));
                }
            }
        }

        [NonSerialized] private string _cdItemIn = "";
        [ColumnName("CD_ITEM_IN")]
        [Required]
        public string CdItemIn
        {
            get => _cdItemIn;
            set
            {
                if (!Equals(_cdItemIn, value))
                {
                    _cdItemIn = value;
                    AddModifiedProperty(nameof(CdItemIn));
                }
            }
        }

    }
}
