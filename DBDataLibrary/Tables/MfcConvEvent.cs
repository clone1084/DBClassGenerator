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
    [TableName("MFC_CONV_EVENT")]
    public partial class MfcConvEvent : ACrudBase<MfcConvEvent>
    {
        public MfcConvEvent() : base() { }
        
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

        [NonSerialized] private int? _oidType;
        [ColumnName("OID_TYPE")]
        public int? OidType
        {
            get => _oidType;
            set
            {
                if (!Equals(_oidType, value))
                {
                    _oidType = value;
                    AddModifiedProperty(nameof(OidType));
                }
            }
        }

        [NonSerialized] private int? _status;
        [ColumnName("STATUS")]
        public int? Status
        {
            get => _status;
            set
            {
                if (!Equals(_status, value))
                {
                    _status = value;
                    AddModifiedProperty(nameof(Status));
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

        [NonSerialized] private string _insertedBy;
        [ColumnName("INSERTED_BY")]
        public string InsertedBy
        {
            get => _insertedBy;
            set
            {
                if (!Equals(_insertedBy, value))
                {
                    _insertedBy = value;
                    AddModifiedProperty(nameof(InsertedBy));
                }
            }
        }

    }
}
