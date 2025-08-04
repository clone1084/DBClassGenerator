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
    [TableName("MFC_CONV_PICK_TO_LIGHT")]
    public partial class MfcConvPickToLight : ACrudBase<MfcConvPickToLight>
    {
        public MfcConvPickToLight() : base() { }
        
        [NonSerialized] private string _cdItem;
        [ColumnName("CD_ITEM")]
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

        [NonSerialized] private int? _stElaborated;
        [ColumnName("ST_ELABORATED")]
        public int? StElaborated
        {
            get => _stElaborated;
            set
            {
                if (!Equals(_stElaborated, value))
                {
                    _stElaborated = value;
                    AddModifiedProperty(nameof(StElaborated));
                }
            }
        }

    }
}
