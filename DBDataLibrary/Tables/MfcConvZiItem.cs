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
    [TableName("MFC_CONV_ZI_ITEM")]
    public partial class MfcConvZiItem : ACrudBase<MfcConvZiItem>
    {
        public MfcConvZiItem() : base() { }
        
        [NonSerialized] private string _sourceItem;
        [ColumnName("SOURCE_ITEM")]
        public string SourceItem
        {
            get => _sourceItem;
            set
            {
                if (!Equals(_sourceItem, value))
                {
                    _sourceItem = value;
                    AddModifiedProperty(nameof(SourceItem));
                }
            }
        }

        [NonSerialized] private string _sourceDescr;
        [ColumnName("SOURCE_DESCR")]
        public string SourceDescr
        {
            get => _sourceDescr;
            set
            {
                if (!Equals(_sourceDescr, value))
                {
                    _sourceDescr = value;
                    AddModifiedProperty(nameof(SourceDescr));
                }
            }
        }

        [NonSerialized] private int? _destItemMin;
        [ColumnName("DEST_ITEM_MIN")]
        public int? DestItemMin
        {
            get => _destItemMin;
            set
            {
                if (!Equals(_destItemMin, value))
                {
                    _destItemMin = value;
                    AddModifiedProperty(nameof(DestItemMin));
                }
            }
        }

        [NonSerialized] private int? _destItemMax;
        [ColumnName("DEST_ITEM_MAX")]
        public int? DestItemMax
        {
            get => _destItemMax;
            set
            {
                if (!Equals(_destItemMax, value))
                {
                    _destItemMax = value;
                    AddModifiedProperty(nameof(DestItemMax));
                }
            }
        }

        [NonSerialized] private string _destDescr;
        [ColumnName("DEST_DESCR")]
        public string DestDescr
        {
            get => _destDescr;
            set
            {
                if (!Equals(_destDescr, value))
                {
                    _destDescr = value;
                    AddModifiedProperty(nameof(DestDescr));
                }
            }
        }

        [NonSerialized] private int? _useNerak;
        [ColumnName("USE_NERAK")]
        public int? UseNerak
        {
            get => _useNerak;
            set
            {
                if (!Equals(_useNerak, value))
                {
                    _useNerak = value;
                    AddModifiedProperty(nameof(UseNerak));
                }
            }
        }

        [NonSerialized] private string _ziItem;
        [ColumnName("ZI_ITEM")]
        public string ZiItem
        {
            get => _ziItem;
            set
            {
                if (!Equals(_ziItem, value))
                {
                    _ziItem = value;
                    AddModifiedProperty(nameof(ZiItem));
                }
            }
        }

        [NonSerialized] private int? _isAscent;
        [ColumnName("IS_ASCENT")]
        public int? IsAscent
        {
            get => _isAscent;
            set
            {
                if (!Equals(_isAscent, value))
                {
                    _isAscent = value;
                    AddModifiedProperty(nameof(IsAscent));
                }
            }
        }

        [NonSerialized] private int? _useOnMi0;
        [ColumnName("USE_ON_MI_0")]
        public int? UseOnMi0
        {
            get => _useOnMi0;
            set
            {
                if (!Equals(_useOnMi0, value))
                {
                    _useOnMi0 = value;
                    AddModifiedProperty(nameof(UseOnMi0));
                }
            }
        }

        [NonSerialized] private string _ziItemPlc2;
        [ColumnName("ZI_ITEM_PLC2")]
        public string ZiItemPlc2
        {
            get => _ziItemPlc2;
            set
            {
                if (!Equals(_ziItemPlc2, value))
                {
                    _ziItemPlc2 = value;
                    AddModifiedProperty(nameof(ZiItemPlc2));
                }
            }
        }

    }
}
