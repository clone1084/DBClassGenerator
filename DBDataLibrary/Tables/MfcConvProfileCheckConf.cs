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
    [TableName("MFC_CONV_PROFILE_CHECK_CONF")]
    public partial class MfcConvProfileCheckConf : ACrudBase<MfcConvProfileCheckConf>
    {
        public MfcConvProfileCheckConf() : base() { }
        
        [NonSerialized] private string _cdItem;
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

        [NonSerialized] private string _parameter;
        [ColumnName("PARAMETER")]
        [Key]
        public string Parameter
        {
            get => _parameter;
            set
            {
                if (!Equals(_parameter, value))
                {
                    _parameter = value;
                    AddModifiedProperty(nameof(Parameter));
                }
            }
        }

        [NonSerialized] private string _value = "";
        [ColumnName("VALUE")]
        [Required]
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
