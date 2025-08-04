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
    [TableName("MFC_CONV_MOV_ZI_TO_SEND")]
    public partial class MfcConvMovZiToSend : ACrudBase<MfcConvMovZiToSend>
    {
        public MfcConvMovZiToSend() : base() { }
        
        [NonSerialized] private long _oidUdm = default(long);
        [ColumnName("OID_UDM")]
        [Required]
        public long OidUdm
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

        [NonSerialized] private DateTime _dtInsert = DateTime.MinValue;
        [ColumnName("DT_INSERT")]
        [Required]
        public DateTime DtInsert
        {
            get => _dtInsert;
            set
            {
                if (!Equals(_dtInsert, value))
                {
                    _dtInsert = value;
                    AddModifiedProperty(nameof(DtInsert));
                }
            }
        }

        [NonSerialized] private string _posTo = "";
        [ColumnName("POS_TO")]
        [Required]
        public string PosTo
        {
            get => _posTo;
            set
            {
                if (!Equals(_posTo, value))
                {
                    _posTo = value;
                    AddModifiedProperty(nameof(PosTo));
                }
            }
        }

        [NonSerialized] private string _code = "";
        [ColumnName("CODE")]
        [Required]
        public string Code
        {
            get => _code;
            set
            {
                if (!Equals(_code, value))
                {
                    _code = value;
                    AddModifiedProperty(nameof(Code));
                }
            }
        }

        [NonSerialized] private int? _enabled;
        [ColumnName("ENABLED")]
        public int? Enabled
        {
            get => _enabled;
            set
            {
                if (!Equals(_enabled, value))
                {
                    _enabled = value;
                    AddModifiedProperty(nameof(Enabled));
                }
            }
        }

    }
}
