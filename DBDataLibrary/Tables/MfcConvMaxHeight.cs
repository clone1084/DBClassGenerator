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
    [TableName("MFC_CONV_MAX_HEIGHT")]
    public partial class MfcConvMaxHeight : ACrudBase<MfcConvMaxHeight>
    {
        public MfcConvMaxHeight() : base() { }
        
        [NonSerialized] private string _itemStart;
        [ColumnName("ITEM_START")]
        public string ItemStart
        {
            get => _itemStart;
            set
            {
                if (!Equals(_itemStart, value))
                {
                    _itemStart = value;
                    AddModifiedProperty(nameof(ItemStart));
                }
            }
        }

        [NonSerialized] private string _itemDest;
        [ColumnName("ITEM_DEST")]
        public string ItemDest
        {
            get => _itemDest;
            set
            {
                if (!Equals(_itemDest, value))
                {
                    _itemDest = value;
                    AddModifiedProperty(nameof(ItemDest));
                }
            }
        }

        [NonSerialized] private string _cdParameter;
        [ColumnName("CD_PARAMETER")]
        public string CdParameter
        {
            get => _cdParameter;
            set
            {
                if (!Equals(_cdParameter, value))
                {
                    _cdParameter = value;
                    AddModifiedProperty(nameof(CdParameter));
                }
            }
        }

    }
}
