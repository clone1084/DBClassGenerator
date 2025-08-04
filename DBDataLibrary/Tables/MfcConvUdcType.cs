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
    [TableName("MFC_CONV_UDC_TYPE")]
    public partial class MfcConvUdcType : ACrudBase<MfcConvUdcType>
    {
        public MfcConvUdcType() : base() { }
        
        [NonSerialized] private long? _oid;
        [ColumnName("OID")]
        public long? Oid
        {
            get => _oid;
            set
            {
                if (!Equals(_oid, value))
                {
                    _oid = value;
                    AddModifiedProperty(nameof(Oid));
                }
            }
        }

        [NonSerialized] private long? _oidProperty;
        [ColumnName("OID_PROPERTY")]
        public long? OidProperty
        {
            get => _oidProperty;
            set
            {
                if (!Equals(_oidProperty, value))
                {
                    _oidProperty = value;
                    AddModifiedProperty(nameof(OidProperty));
                }
            }
        }

        [NonSerialized] private int? _tpUdc;
        [ColumnName("TP_UDC")]
        public int? TpUdc
        {
            get => _tpUdc;
            set
            {
                if (!Equals(_tpUdc, value))
                {
                    _tpUdc = value;
                    AddModifiedProperty(nameof(TpUdc));
                }
            }
        }

        [NonSerialized] private int? _idUdc;
        [ColumnName("ID_UDC")]
        public int? IdUdc
        {
            get => _idUdc;
            set
            {
                if (!Equals(_idUdc, value))
                {
                    _idUdc = value;
                    AddModifiedProperty(nameof(IdUdc));
                }
            }
        }

        [NonSerialized] private string _dscUdc;
        [ColumnName("DSC_UDC")]
        public string DscUdc
        {
            get => _dscUdc;
            set
            {
                if (!Equals(_dscUdc, value))
                {
                    _dscUdc = value;
                    AddModifiedProperty(nameof(DscUdc));
                }
            }
        }

        [NonSerialized] private int? _width;
        [ColumnName("WIDTH")]
        public int? Width
        {
            get => _width;
            set
            {
                if (!Equals(_width, value))
                {
                    _width = value;
                    AddModifiedProperty(nameof(Width));
                }
            }
        }

        [NonSerialized] private int? _length;
        [ColumnName("LENGTH")]
        public int? Length
        {
            get => _length;
            set
            {
                if (!Equals(_length, value))
                {
                    _length = value;
                    AddModifiedProperty(nameof(Length));
                }
            }
        }

        [NonSerialized] private int? _weight;
        [ColumnName("WEIGHT")]
        public int? Weight
        {
            get => _weight;
            set
            {
                if (!Equals(_weight, value))
                {
                    _weight = value;
                    AddModifiedProperty(nameof(Weight));
                }
            }
        }

        [NonSerialized] private int? _lenX;
        [ColumnName("LEN_X")]
        public int? LenX
        {
            get => _lenX;
            set
            {
                if (!Equals(_lenX, value))
                {
                    _lenX = value;
                    AddModifiedProperty(nameof(LenX));
                }
            }
        }

        [NonSerialized] private int? _lenY;
        [ColumnName("LEN_Y")]
        public int? LenY
        {
            get => _lenY;
            set
            {
                if (!Equals(_lenY, value))
                {
                    _lenY = value;
                    AddModifiedProperty(nameof(LenY));
                }
            }
        }

        [NonSerialized] private int? _lenZ;
        [ColumnName("LEN_Z")]
        public int? LenZ
        {
            get => _lenZ;
            set
            {
                if (!Equals(_lenZ, value))
                {
                    _lenZ = value;
                    AddModifiedProperty(nameof(LenZ));
                }
            }
        }

        [NonSerialized] private int? _maxWeight;
        [ColumnName("MAX_WEIGHT")]
        public int? MaxWeight
        {
            get => _maxWeight;
            set
            {
                if (!Equals(_maxWeight, value))
                {
                    _maxWeight = value;
                    AddModifiedProperty(nameof(MaxWeight));
                }
            }
        }

        [NonSerialized] private int? _tare;
        [ColumnName("TARE")]
        public int? Tare
        {
            get => _tare;
            set
            {
                if (!Equals(_tare, value))
                {
                    _tare = value;
                    AddModifiedProperty(nameof(Tare));
                }
            }
        }

        [NonSerialized] private int? _doubleDepth;
        [ColumnName("DOUBLE_DEPTH")]
        public int? DoubleDepth
        {
            get => _doubleDepth;
            set
            {
                if (!Equals(_doubleDepth, value))
                {
                    _doubleDepth = value;
                    AddModifiedProperty(nameof(DoubleDepth));
                }
            }
        }

        [NonSerialized] private string _tpUdcHost;
        [ColumnName("TP_UDC_HOST")]
        public string TpUdcHost
        {
            get => _tpUdcHost;
            set
            {
                if (!Equals(_tpUdcHost, value))
                {
                    _tpUdcHost = value;
                    AddModifiedProperty(nameof(TpUdcHost));
                }
            }
        }

        [NonSerialized] private string _dscUdcRf;
        [ColumnName("DSC_UDC_RF")]
        public string DscUdcRf
        {
            get => _dscUdcRf;
            set
            {
                if (!Equals(_dscUdcRf, value))
                {
                    _dscUdcRf = value;
                    AddModifiedProperty(nameof(DscUdcRf));
                }
            }
        }

        [NonSerialized] private int? _tpUdcPlc;
        [ColumnName("TP_UDC_PLC")]
        public int? TpUdcPlc
        {
            get => _tpUdcPlc;
            set
            {
                if (!Equals(_tpUdcPlc, value))
                {
                    _tpUdcPlc = value;
                    AddModifiedProperty(nameof(TpUdcPlc));
                }
            }
        }

        [NonSerialized] private string _cdLanguage;
        [ColumnName("CD_LANGUAGE")]
        public string CdLanguage
        {
            get => _cdLanguage;
            set
            {
                if (!Equals(_cdLanguage, value))
                {
                    _cdLanguage = value;
                    AddModifiedProperty(nameof(CdLanguage));
                }
            }
        }

    }
}
