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
    [TableName("MFC_CONV_CODE_ITEM")]
    public partial class MfcConvCodeItem : ACrudBase<MfcConvCodeItem>
    {
        public MfcConvCodeItem() : base() { }
        
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

        [NonSerialized] private string _dscItem;
        [ColumnName("DSC_ITEM")]
        public string DscItem
        {
            get => _dscItem;
            set
            {
                if (!Equals(_dscItem, value))
                {
                    _dscItem = value;
                    AddModifiedProperty(nameof(DscItem));
                }
            }
        }

        [NonSerialized] private int? _floor;
        [ColumnName("FLOOR")]
        public int? Floor
        {
            get => _floor;
            set
            {
                if (!Equals(_floor, value))
                {
                    _floor = value;
                    AddModifiedProperty(nameof(Floor));
                }
            }
        }

    }
}
