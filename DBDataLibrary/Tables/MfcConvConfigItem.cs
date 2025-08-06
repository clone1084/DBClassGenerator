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
    [TableName("MFC_CONV_CONFIG_ITEM")]
    public partial class MfcConvConfigItem : ACrudBase<MfcConvConfigItem>
    {
        public MfcConvConfigItem() : base() { }
        
        [NonSerialized] private string _cdItem = "";
        [ColumnName("CD_ITEM")]
        [Key]
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

        [NonSerialized] private string _constrain = "";
        [ColumnName("CONSTRAIN")]
        [Key]
        public string Constrain
        {
            get => _constrain;
            set
            {
                if (!Equals(_constrain, value))
                {
                    _constrain = value;
                    AddModifiedProperty(nameof(Constrain));
                }
            }
        }

        [NonSerialized] private int _tpItem = default(int);
        [ColumnName("TP_ITEM")]
        [Key]
        public int TpItem
        {
            get => _tpItem;
            set
            {
                if (!Equals(_tpItem, value))
                {
                    _tpItem = value;
                    AddModifiedProperty(nameof(TpItem));
                }
            }
        }

        [NonSerialized] private int? _ack;
        [ColumnName("ACK")]
        public int? Ack
        {
            get => _ack;
            set
            {
                if (!Equals(_ack, value))
                {
                    _ack = value;
                    AddModifiedProperty(nameof(Ack));
                }
            }
        }

        [NonSerialized] private long? _oidAreasLoc;
        [ColumnName("OID_AREAS_LOC")]
        public long? OidAreasLoc
        {
            get => _oidAreasLoc;
            set
            {
                if (!Equals(_oidAreasLoc, value))
                {
                    _oidAreasLoc = value;
                    AddModifiedProperty(nameof(OidAreasLoc));
                }
            }
        }

    }
}
