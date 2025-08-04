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
    [TableName("MFC_CONV_ITEM_REGION")]
    public partial class MfcConvItemRegion : ACrudBase<MfcConvItemRegion>
    {
        public MfcConvItemRegion() : base() { }
        
        [NonSerialized] private string _cdItem;
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

        [NonSerialized] private int? _region;
        [ColumnName("REGION")]
        public int? Region
        {
            get => _region;
            set
            {
                if (!Equals(_region, value))
                {
                    _region = value;
                    AddModifiedProperty(nameof(Region));
                }
            }
        }

    }
}
