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
    [TableName("MFC_CONV_EMPTY_LINE_DETAILS")]
    public partial class MfcConvEmptyLineDetails : ACrudBase<MfcConvEmptyLineDetails>
    {
        public MfcConvEmptyLineDetails() : base() { }
        
        [NonSerialized] private int _cdLine = default(int);
        [ColumnName("CD_LINE")]
        [Required]
        public int CdLine
        {
            get => _cdLine;
            set
            {
                if (!Equals(_cdLine, value))
                {
                    _cdLine = value;
                    AddModifiedProperty(nameof(CdLine));
                }
            }
        }

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

        [NonSerialized] private int? _posStatus;
        [ColumnName("POS_STATUS")]
        public int? PosStatus
        {
            get => _posStatus;
            set
            {
                if (!Equals(_posStatus, value))
                {
                    _posStatus = value;
                    AddModifiedProperty(nameof(PosStatus));
                }
            }
        }

        [NonSerialized] private int? _full;
        [ColumnName("FULL")]
        public int? Full
        {
            get => _full;
            set
            {
                if (!Equals(_full, value))
                {
                    _full = value;
                    AddModifiedProperty(nameof(Full));
                }
            }
        }

        [NonSerialized] private string _typeMsg = "";
        [ColumnName("TYPE_MSG")]
        public string TypeMsg
        {
            get => _typeMsg;
            set
            {
                if (!Equals(_typeMsg, value))
                {
                    _typeMsg = value;
                    AddModifiedProperty(nameof(TypeMsg));
                }
            }
        }

    }
}
