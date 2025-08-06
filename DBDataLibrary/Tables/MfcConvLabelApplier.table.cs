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
    [TableName("MFC_CONV_LABEL_APPLIER")]
    public partial class MfcConvLabelApplier : ACrudBase<MfcConvLabelApplier>
    {
        public MfcConvLabelApplier() : base() { }
        
        [NonSerialized] private string _cdItem = "";
        [ColumnName("CD_ITEM")]
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

        [NonSerialized] private int? _flStatus;
        [ColumnName("FL_STATUS")]
        public int? FlStatus
        {
            get => _flStatus;
            set
            {
                if (!Equals(_flStatus, value))
                {
                    _flStatus = value;
                    AddModifiedProperty(nameof(FlStatus));
                }
            }
        }

    }
}
