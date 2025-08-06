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
    [TableName("MFC_CONV_ITEM_TO_REJECT")]
    public partial class MfcConvItemToReject : ACrudBase<MfcConvItemToReject>
    {
        public MfcConvItemToReject() : base() { }
        
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

        [NonSerialized] private string _constrain = "";
        [ColumnName("CONSTRAIN")]
        [Required]
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

        [NonSerialized] private string _cdRejectItem = "";
        [ColumnName("CD_REJECT_ITEM")]
        [Required]
        public string CdRejectItem
        {
            get => _cdRejectItem;
            set
            {
                if (!Equals(_cdRejectItem, value))
                {
                    _cdRejectItem = value;
                    AddModifiedProperty(nameof(CdRejectItem));
                }
            }
        }

    }
}
