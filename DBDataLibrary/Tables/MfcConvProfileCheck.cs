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
    [TableName("MFC_CONV_PROFILE_CHECK")]
    public partial class MfcConvProfileCheck : ACrudBase<MfcConvProfileCheck>
    {
        public MfcConvProfileCheck() : base() { }
        
        [NonSerialized] private string _cdItem = "";
        [ColumnName("CD_ITEM")]
        [Key]
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

        [NonSerialized] private string _cdField = "";
        [ColumnName("CD_FIELD")]
        [Key]
        public string CdField
        {
            get => _cdField;
            set
            {
                if (!Equals(_cdField, value))
                {
                    _cdField = value;
                    AddModifiedProperty(nameof(CdField));
                }
            }
        }

        [NonSerialized] private string _value = "";
        [ColumnName("VALUE")]
        public string Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    AddModifiedProperty(nameof(Value));
                }
            }
        }

    }
}
