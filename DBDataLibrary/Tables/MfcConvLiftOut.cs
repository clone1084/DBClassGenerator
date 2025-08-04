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
    [TableName("MFC_CONV_LIFT_OUT")]
    public partial class MfcConvLiftOut : ACrudBase<MfcConvLiftOut>
    {
        public MfcConvLiftOut() : base() { }
        
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

        [NonSerialized] private string _cdItem = "";
        [ColumnName("CD_ITEM")]
        [Required]
        public string CdItem
        {
            get => _cdItem;
            set
            {
                if (!Equals(_cdItem, value))
                {
                    _cdItem = value;
                    AddModifiedProperty(nameof(CdItem));
                }
            }
        }

    }
}
