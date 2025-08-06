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
    [TableName("MFC_CONV_MULTIF_FORM_DATA")]
    public partial class MfcConvMultifFormData : ACrudBase<MfcConvMultifFormData>
    {
        public MfcConvMultifFormData() : base() { }
        
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

        [NonSerialized] private string _cdUdc = "";
        [ColumnName("CD_UDC")]
        public string CdUdc
        {
            get => _cdUdc;
            set
            {
                if (!Equals(_cdUdc, value))
                {
                    _cdUdc = value;
                    AddModifiedProperty(nameof(CdUdc));
                }
            }
        }

        [NonSerialized] private string _cdUdm = "";
        [ColumnName("CD_UDM")]
        public string CdUdm
        {
            get => _cdUdm;
            set
            {
                if (!Equals(_cdUdm, value))
                {
                    _cdUdm = value;
                    AddModifiedProperty(nameof(CdUdm));
                }
            }
        }

        [NonSerialized] private string _flReader = "";
        [ColumnName("FL_READER")]
        public string FlReader
        {
            get => _flReader;
            set
            {
                if (!Equals(_flReader, value))
                {
                    _flReader = value;
                    AddModifiedProperty(nameof(FlReader));
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

        [NonSerialized] private string _cdArticle = "";
        [ColumnName("CD_ARTICLE")]
        public string CdArticle
        {
            get => _cdArticle;
            set
            {
                if (!Equals(_cdArticle, value))
                {
                    _cdArticle = value;
                    AddModifiedProperty(nameof(CdArticle));
                }
            }
        }

        [NonSerialized] private string _dscArticle = "";
        [ColumnName("DSC_ARTICLE")]
        public string DscArticle
        {
            get => _dscArticle;
            set
            {
                if (!Equals(_dscArticle, value))
                {
                    _dscArticle = value;
                    AddModifiedProperty(nameof(DscArticle));
                }
            }
        }

        [NonSerialized] private long? _oidOrder;
        [ColumnName("OID_ORDER")]
        public long? OidOrder
        {
            get => _oidOrder;
            set
            {
                if (!Equals(_oidOrder, value))
                {
                    _oidOrder = value;
                    AddModifiedProperty(nameof(OidOrder));
                }
            }
        }

        [NonSerialized] private long? _oidOrderR;
        [ColumnName("OID_ORDER_R")]
        public long? OidOrderR
        {
            get => _oidOrderR;
            set
            {
                if (!Equals(_oidOrderR, value))
                {
                    _oidOrderR = value;
                    AddModifiedProperty(nameof(OidOrderR));
                }
            }
        }

        [NonSerialized] private long? _udcQty;
        [ColumnName("UDC_QTY")]
        public long? UdcQty
        {
            get => _udcQty;
            set
            {
                if (!Equals(_udcQty, value))
                {
                    _udcQty = value;
                    AddModifiedProperty(nameof(UdcQty));
                }
            }
        }

        [NonSerialized] private int? _udcSpare;
        [ColumnName("UDC_SPARE")]
        public int? UdcSpare
        {
            get => _udcSpare;
            set
            {
                if (!Equals(_udcSpare, value))
                {
                    _udcSpare = value;
                    AddModifiedProperty(nameof(UdcSpare));
                }
            }
        }

        [NonSerialized] private string _cdOrder = "";
        [ColumnName("CD_ORDER")]
        public string CdOrder
        {
            get => _cdOrder;
            set
            {
                if (!Equals(_cdOrder, value))
                {
                    _cdOrder = value;
                    AddModifiedProperty(nameof(CdOrder));
                }
            }
        }

        [NonSerialized] private int? _isToLight;
        [ColumnName("IS_TO_LIGHT")]
        public int? IsToLight
        {
            get => _isToLight;
            set
            {
                if (!Equals(_isToLight, value))
                {
                    _isToLight = value;
                    AddModifiedProperty(nameof(IsToLight));
                }
            }
        }

        [NonSerialized] private long? _qtyToPick;
        [ColumnName("QTY_TO_PICK")]
        public long? QtyToPick
        {
            get => _qtyToPick;
            set
            {
                if (!Equals(_qtyToPick, value))
                {
                    _qtyToPick = value;
                    AddModifiedProperty(nameof(QtyToPick));
                }
            }
        }

        [NonSerialized] private int? _flTaken;
        [ColumnName("FL_TAKEN")]
        public int? FlTaken
        {
            get => _flTaken;
            set
            {
                if (!Equals(_flTaken, value))
                {
                    _flTaken = value;
                    AddModifiedProperty(nameof(FlTaken));
                }
            }
        }

        [NonSerialized] private string _cdItemFifo = "";
        [ColumnName("CD_ITEM_FIFO")]
        public string CdItemFifo
        {
            get => _cdItemFifo;
            set
            {
                if (!Equals(_cdItemFifo, value))
                {
                    _cdItemFifo = value;
                    AddModifiedProperty(nameof(CdItemFifo));
                }
            }
        }

        [NonSerialized] private int? _flPicked;
        [ColumnName("FL_PICKED")]
        public int? FlPicked
        {
            get => _flPicked;
            set
            {
                if (!Equals(_flPicked, value))
                {
                    _flPicked = value;
                    AddModifiedProperty(nameof(FlPicked));
                }
            }
        }

        [NonSerialized] private DateTime? _dtPicking;
        [ColumnName("DT_PICKING")]
        public DateTime? DtPicking
        {
            get => _dtPicking;
            set
            {
                if (!Equals(_dtPicking, value))
                {
                    _dtPicking = value;
                    AddModifiedProperty(nameof(DtPicking));
                }
            }
        }

    }
}
