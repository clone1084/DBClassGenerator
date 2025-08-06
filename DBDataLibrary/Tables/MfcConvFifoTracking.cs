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
    [TableName("MFC_CONV_FIFO_TRACKING")]
    public partial class MfcConvFifoTracking : ACrudBase<MfcConvFifoTracking>
    {
        public MfcConvFifoTracking() : base() { }
        
        [NonSerialized] private int _oid = default(int);
        [ColumnName("OID")]
        [Key]
        public int Oid
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

        [NonSerialized] private string _fifoType = "";
        [ColumnName("FIFO_TYPE")]
        [Required]
        public string FifoType
        {
            get => _fifoType;
            set
            {
                if (!Equals(_fifoType, value))
                {
                    _fifoType = value;
                    AddModifiedProperty(nameof(FifoType));
                }
            }
        }

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

    }
}
