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
    [TableName("MFC_CONV_MOVEMENTS_ERRORS")]
    public partial class MfcConvMovementsErrors : ACrudBase<MfcConvMovementsErrors>
    {
        public MfcConvMovementsErrors() : base() { }
        
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

        [NonSerialized] private long _oidMovement = default(long);
        [ColumnName("OID_MOVEMENT")]
        [Required]
        public long OidMovement
        {
            get => _oidMovement;
            set
            {
                if (!Equals(_oidMovement, value))
                {
                    _oidMovement = value;
                    AddModifiedProperty(nameof(OidMovement));
                }
            }
        }

        [NonSerialized] private long? _result;
        [ColumnName("RESULT")]
        public long? Result
        {
            get => _result;
            set
            {
                if (!Equals(_result, value))
                {
                    _result = value;
                    AddModifiedProperty(nameof(Result));
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

    }
}
