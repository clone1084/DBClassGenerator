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
    [TableName("MFC_CONV_READDRESS")]
    public partial class MfcConvReaddress : ACrudBase<MfcConvReaddress>
    {
        public MfcConvReaddress() : base() { }
        
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

        [NonSerialized] private string _cdDestination = "";
        [ColumnName("CD_DESTINATION")]
        [Required]
        public string CdDestination
        {
            get => _cdDestination;
            set
            {
                if (!Equals(_cdDestination, value))
                {
                    _cdDestination = value;
                    AddModifiedProperty(nameof(CdDestination));
                }
            }
        }

    }
}
