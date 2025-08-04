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
    [TableName("MFC_CONV_FIFO_DESC")]
    public partial class MfcConvFifoDesc : ACrudBase<MfcConvFifoDesc>
    {
        public MfcConvFifoDesc() : base() { }
        
        [NonSerialized] private int _cdFifo = default(int);
        [ColumnName("CD_FIFO")]
        [Required]
        public int CdFifo
        {
            get => _cdFifo;
            set
            {
                if (!Equals(_cdFifo, value))
                {
                    _cdFifo = value;
                    AddModifiedProperty(nameof(CdFifo));
                }
            }
        }

        [NonSerialized] private string _cdItemHead = "";
        [ColumnName("CD_ITEM_HEAD")]
        [Required]
        public string CdItemHead
        {
            get => _cdItemHead;
            set
            {
                if (!Equals(_cdItemHead, value))
                {
                    _cdItemHead = value;
                    AddModifiedProperty(nameof(CdItemHead));
                }
            }
        }

        [NonSerialized] private string _cdItemTail = "";
        [ColumnName("CD_ITEM_TAIL")]
        [Required]
        public string CdItemTail
        {
            get => _cdItemTail;
            set
            {
                if (!Equals(_cdItemTail, value))
                {
                    _cdItemTail = value;
                    AddModifiedProperty(nameof(CdItemTail));
                }
            }
        }

        [NonSerialized] private int? _numUdm;
        [ColumnName("NUM_UDM")]
        public int? NumUdm
        {
            get => _numUdm;
            set
            {
                if (!Equals(_numUdm, value))
                {
                    _numUdm = value;
                    AddModifiedProperty(nameof(NumUdm));
                }
            }
        }

    }
}
