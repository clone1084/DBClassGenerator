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
    [TableName("MFC_CONV_ROUTING")]
    public partial class MfcConvRouting : ACrudBase<MfcConvRouting>
    {
        public MfcConvRouting() : base() { }
        
        [NonSerialized] private string _cdItemFrom;
        [ColumnName("CD_ITEM_FROM")]
        [Key]
        public string CdItemFrom
        {
            get => _cdItemFrom;
            set
            {
                if (!Equals(_cdItemFrom, value))
                {
                    _cdItemFrom = value;
                    AddModifiedProperty(nameof(CdItemFrom));
                }
            }
        }

        [NonSerialized] private string _cdItemTo;
        [ColumnName("CD_ITEM_TO")]
        [Key]
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

        [NonSerialized] private int _ack = default(int);
        [ColumnName("ACK")]
        [Required]
        public int Ack
        {
            get => _ack;
            set
            {
                if (!Equals(_ack, value))
                {
                    _ack = value;
                    AddModifiedProperty(nameof(Ack));
                }
            }
        }

    }
}
