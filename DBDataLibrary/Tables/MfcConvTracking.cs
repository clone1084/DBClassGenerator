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
    [TableName("MFC_CONV_TRACKING")]
    public partial class MfcConvTracking : ACrudBase<MfcConvTracking>
    {
        public MfcConvTracking() : base() { }
        
        [NonSerialized] private long? _oidUdm;
        [ColumnName("OID_UDM")]
        public long? OidUdm
        {
            get => _oidUdm;
            set
            {
                if (!Equals(_oidUdm, value))
                {
                    _oidUdm = value;
                    AddModifiedProperty(nameof(OidUdm));
                }
            }
        }

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

        [NonSerialized] private string _cdGroup;
        [ColumnName("CD_GROUP")]
        public string CdGroup
        {
            get => _cdGroup;
            set
            {
                if (!Equals(_cdGroup, value))
                {
                    _cdGroup = value;
                    AddModifiedProperty(nameof(CdGroup));
                }
            }
        }

        [NonSerialized] private int _posOccupied = default(int);
        [ColumnName("POS_OCCUPIED")]
        [Required]
        public int PosOccupied
        {
            get => _posOccupied;
            set
            {
                if (!Equals(_posOccupied, value))
                {
                    _posOccupied = value;
                    AddModifiedProperty(nameof(PosOccupied));
                }
            }
        }

        [NonSerialized] private int _aMessage = default(int);
        [ColumnName("A_MESSAGE")]
        [Required]
        public int AMessage
        {
            get => _aMessage;
            set
            {
                if (!Equals(_aMessage, value))
                {
                    _aMessage = value;
                    AddModifiedProperty(nameof(AMessage));
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

    }
}
