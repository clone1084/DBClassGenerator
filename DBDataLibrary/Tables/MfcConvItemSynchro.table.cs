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
    [TableName("MFC_CONV_ITEM_SYNCHRO")]
    public partial class MfcConvItemSynchro : ACrudBase<MfcConvItemSynchro>
    {
        public MfcConvItemSynchro() : base() { }
        
        [NonSerialized] private string _cdItem = "";
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

        [NonSerialized] private int? _flPltReady;
        [ColumnName("FL_PLT_READY")]
        public int? FlPltReady
        {
            get => _flPltReady;
            set
            {
                if (!Equals(_flPltReady, value))
                {
                    _flPltReady = value;
                    AddModifiedProperty(nameof(FlPltReady));
                }
            }
        }

        [NonSerialized] private int? _oidUdm;
        [ColumnName("OID_UDM")]
        public int? OidUdm
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

        [NonSerialized] private DateTime _dtUpdate = DateTime.MinValue;
        [ColumnName("DT_UPDATE")]
        [Required]
        public DateTime DtUpdate
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
