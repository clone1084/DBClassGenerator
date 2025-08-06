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
    [TableName("MFC_CONV_EVENT_ITEM")]
    public partial class MfcConvEventItem : ACrudBase<MfcConvEventItem>
    {
        public MfcConvEventItem() : base() { }
        
        [NonSerialized] private long _oid = default(long);
        [ColumnName("OID")]
        [Key]
        public long Oid
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

        [NonSerialized] private long _oidType = default(long);
        [ColumnName("OID_TYPE")]
        [Required]
        public long OidType
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

    }
}
