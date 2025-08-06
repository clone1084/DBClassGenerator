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
    [TableName("MFC_CONV_UDC_REQUESTS")]
    public partial class MfcConvUdcRequests : ACrudBase<MfcConvUdcRequests>
    {
        public MfcConvUdcRequests() : base() { }
        
        [NonSerialized] private string _barcode = "";
        [ColumnName("BARCODE")]
        public string Barcode
        {
            get => _barcode;
            set
            {
                if (!Equals(_barcode, value))
                {
                    _barcode = value;
                    AddModifiedProperty(nameof(Barcode));
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

        [NonSerialized] private int _stElab = default(int);
        [ColumnName("ST_ELAB")]
        [Required]
        public int StElab
        {
            get => _stElab;
            set
            {
                if (!Equals(_stElab, value))
                {
                    _stElab = value;
                    AddModifiedProperty(nameof(StElab));
                }
            }
        }

        [NonSerialized] private string _guid = "";
        [ColumnName("GUID")]
        [Key]
        public string Guid
        {
            get => _guid;
            set
            {
                if (!Equals(_guid, value))
                {
                    _guid = value;
                    AddModifiedProperty(nameof(Guid));
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

    }
}
