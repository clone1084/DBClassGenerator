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
    [TableName("MFC_CONV_MOVEMENTS_STATUS")]
    public partial class MfcConvMovementsStatus : ACrudBase<MfcConvMovementsStatus>
    {
        public MfcConvMovementsStatus() : base() { }
        
        [NonSerialized] private string _cdIndex;
        [ColumnName("CD_INDEX")]
        [Key]
        public string CdIndex
        {
            get => _cdIndex;
            set
            {
                if (!Equals(_cdIndex, value))
                {
                    _cdIndex = value;
                    AddModifiedProperty(nameof(CdIndex));
                }
            }
        }

        [NonSerialized] private string _fromPos = "";
        [ColumnName("FROM_POS")]
        [Required]
        public string FromPos
        {
            get => _fromPos;
            set
            {
                if (!Equals(_fromPos, value))
                {
                    _fromPos = value;
                    AddModifiedProperty(nameof(FromPos));
                }
            }
        }

        [NonSerialized] private string _toPos = "";
        [ColumnName("TO_POS")]
        [Required]
        public string ToPos
        {
            get => _toPos;
            set
            {
                if (!Equals(_toPos, value))
                {
                    _toPos = value;
                    AddModifiedProperty(nameof(ToPos));
                }
            }
        }

        [NonSerialized] private int? _stMov;
        [ColumnName("ST_MOV")]
        public int? StMov
        {
            get => _stMov;
            set
            {
                if (!Equals(_stMov, value))
                {
                    _stMov = value;
                    AddModifiedProperty(nameof(StMov));
                }
            }
        }

        [NonSerialized] private DateTime? _dtUpdate;
        [ColumnName("DT_UPDATE")]
        public DateTime? DtUpdate
        {
            get => _dtUpdate;
            set
            {
                if (!Equals(_dtUpdate, value))
                {
                    _dtUpdate = value;
                    AddModifiedProperty(nameof(DtUpdate));
                }
            }
        }

        [NonSerialized] private int? _timeout;
        [ColumnName("TIMEOUT")]
        public int? Timeout
        {
            get => _timeout;
            set
            {
                if (!Equals(_timeout, value))
                {
                    _timeout = value;
                    AddModifiedProperty(nameof(Timeout));
                }
            }
        }

    }
}
