using System;
using System.ComponentModel.DataAnnotations;
using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;

namespace DBDataLibrary.Tables
{
    //  --------------------------------------------------
    // --            AUTOMATIC GENERATED CLASS           --
    // --                DO NOT MODIFY!!!                --
    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --
    //  --------------------------------------------------
    [TableName("MFC_CONV_LIFT_LAST_POS")]
    public partial class MfcConvLiftLastPos : ACrudBase<MfcConvLiftLastPos>
    {
        public MfcConvLiftLastPos() : base() { }
        
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

        [NonSerialized] private string _cdLastItem;
        [ColumnName("CD_LAST_ITEM")]
        public string CdLastItem
        {
            get => _cdLastItem;
            set
            {
                if (!Equals(_cdLastItem, value))
                {
                    _cdLastItem = value;
                    AddModifiedProperty(nameof(CdLastItem));
                }
            }
        }

        [NonSerialized] private DateTime? _dtUpdate;
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate
        {
            get => _dtUpdate;
            set
            {
                if (!Equals(_dtUpdate, value))
                {
                    _dtUpdate = value;
                    AddModifiedProperty(nameof(DtUpdate));
                }
            }
        }

    }
}
