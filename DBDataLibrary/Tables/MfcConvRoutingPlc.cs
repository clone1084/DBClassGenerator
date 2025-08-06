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
    [TableName("MFC_CONV_ROUTING_PLC")]
    public partial class MfcConvRoutingPlc : ACrudBase<MfcConvRoutingPlc>
    {
        public MfcConvRoutingPlc() : base() { }
        
        [NonSerialized] private string _cdItemTo = "";
        [ColumnName("CD_ITEM_TO")]
        [Required]
        public string CdItemTo
        {
            get => _cdItemTo;
            set
            {
                if (!Equals(_cdItemTo, value))
                {
                    _cdItemTo = value;
                    AddModifiedProperty(nameof(CdItemTo));
                }
            }
        }

        [NonSerialized] private string _cdItemNext = "";
        [ColumnName("CD_ITEM_NEXT")]
        [Required]
        public string CdItemNext
        {
            get => _cdItemNext;
            set
            {
                if (!Equals(_cdItemNext, value))
                {
                    _cdItemNext = value;
                    AddModifiedProperty(nameof(CdItemNext));
                }
            }
        }

    }
}
