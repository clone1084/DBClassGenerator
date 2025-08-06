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
    [TableName("MFC_CONV_SEND_TO_MULTIFUNCTION")]
    public partial class MfcConvSendToMultifunction : ACrudBase<MfcConvSendToMultifunction>
    {
        public MfcConvSendToMultifunction() : base() { }
        
        [NonSerialized] private long? _oidUdc;
        [ColumnName("OID_UDC")]
        public long? OidUdc
        {
            get => _oidUdc;
            set
            {
                if (!Equals(_oidUdc, value))
                {
                    _oidUdc = value;
                    AddModifiedProperty(nameof(OidUdc));
                }
            }
        }

        [NonSerialized] private int? _codScarto;
        [ColumnName("COD_SCARTO")]
        public int? CodScarto
        {
            get => _codScarto;
            set
            {
                if (!Equals(_codScarto, value))
                {
                    _codScarto = value;
                    AddModifiedProperty(nameof(CodScarto));
                }
            }
        }

        [NonSerialized] private DateTime? _dtInsert;
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert
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

        [NonSerialized] private string _descrScarto = "";
        [ColumnName("DESCR_SCARTO")]
        public string DescrScarto
        {
            get => _descrScarto;
            set
            {
                if (!Equals(_descrScarto, value))
                {
                    _descrScarto = value;
                    AddModifiedProperty(nameof(DescrScarto));
                }
            }
        }

        [NonSerialized] private int? _flDelivered;
        [ColumnName("FL_DELIVERED")]
        public int? FlDelivered
        {
            get => _flDelivered;
            set
            {
                if (!Equals(_flDelivered, value))
                {
                    _flDelivered = value;
                    AddModifiedProperty(nameof(FlDelivered));
                }
            }
        }

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

        [NonSerialized] private int? _idScarto;
        [ColumnName("ID_SCARTO")]
        public int? IdScarto
        {
            get => _idScarto;
            set
            {
                if (!Equals(_idScarto, value))
                {
                    _idScarto = value;
                    AddModifiedProperty(nameof(IdScarto));
                }
            }
        }

    }
}
