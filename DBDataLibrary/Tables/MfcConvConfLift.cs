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
    [TableName("MFC_CONV_CONF_LIFT")]
    public partial class MfcConvConfLift : ACrudBase<MfcConvConfLift>
    {
        public MfcConvConfLift() : base() { }
        
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

        [NonSerialized] private string _cdItem1 = "";
        [ColumnName("CD_ITEM1")]
        [Required]
        public string CdItem1
        {
            get => _cdItem1;
            set
            {
                if (!Equals(_cdItem1, value))
                {
                    _cdItem1 = value;
                    AddModifiedProperty(nameof(CdItem1));
                }
            }
        }

        [NonSerialized] private string _cdItem2;
        [ColumnName("CD_ITEM2")]
        public string CdItem2
        {
            get => _cdItem2;
            set
            {
                if (!Equals(_cdItem2, value))
                {
                    _cdItem2 = value;
                    AddModifiedProperty(nameof(CdItem2));
                }
            }
        }

        [NonSerialized] private string _constrain;
        [ColumnName("CONSTRAIN")]
        public string Constrain
        {
            get => _constrain;
            set
            {
                if (!Equals(_constrain, value))
                {
                    _constrain = value;
                    AddModifiedProperty(nameof(Constrain));
                }
            }
        }

        [NonSerialized] private int? _stEnabled;
        [ColumnName("ST_ENABLED")]
        public int? StEnabled
        {
            get => _stEnabled;
            set
            {
                if (!Equals(_stEnabled, value))
                {
                    _stEnabled = value;
                    AddModifiedProperty(nameof(StEnabled));
                }
            }
        }

        [NonSerialized] private string _cdItemReject;
        [ColumnName("CD_ITEM_REJECT")]
        public string CdItemReject
        {
            get => _cdItemReject;
            set
            {
                if (!Equals(_cdItemReject, value))
                {
                    _cdItemReject = value;
                    AddModifiedProperty(nameof(CdItemReject));
                }
            }
        }

        [NonSerialized] private string _cdItemUnload;
        [ColumnName("CD_ITEM_UNLOAD")]
        public string CdItemUnload
        {
            get => _cdItemUnload;
            set
            {
                if (!Equals(_cdItemUnload, value))
                {
                    _cdItemUnload = value;
                    AddModifiedProperty(nameof(CdItemUnload));
                }
            }
        }

        [NonSerialized] private string _cdItemDefaultPos;
        [ColumnName("CD_ITEM_DEFAULT_POS")]
        public string CdItemDefaultPos
        {
            get => _cdItemDefaultPos;
            set
            {
                if (!Equals(_cdItemDefaultPos, value))
                {
                    _cdItemDefaultPos = value;
                    AddModifiedProperty(nameof(CdItemDefaultPos));
                }
            }
        }

        [NonSerialized] private int? _checkUnloadItem;
        [ColumnName("CHECK_UNLOAD_ITEM")]
        public int? CheckUnloadItem
        {
            get => _checkUnloadItem;
            set
            {
                if (!Equals(_checkUnloadItem, value))
                {
                    _checkUnloadItem = value;
                    AddModifiedProperty(nameof(CheckUnloadItem));
                }
            }
        }

        [NonSerialized] private int? _returnDefaultPosition;
        [ColumnName("RETURN_DEFAULT_POSITION")]
        public int? ReturnDefaultPosition
        {
            get => _returnDefaultPosition;
            set
            {
                if (!Equals(_returnDefaultPosition, value))
                {
                    _returnDefaultPosition = value;
                    AddModifiedProperty(nameof(ReturnDefaultPosition));
                }
            }
        }

    }
}
