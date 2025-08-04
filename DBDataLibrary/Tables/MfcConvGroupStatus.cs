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
    [TableName("MFC_CONV_GROUP_STATUS")]
    public partial class MfcConvGroupStatus : ACrudBase<MfcConvGroupStatus>
    {
        public MfcConvGroupStatus() : base() { }
        
        [NonSerialized] private string _cdGroup;
        [ColumnName("CD_GROUP")]
        [Key]
        public string CdGroup
        {
            get => _cdGroup;
            set
            {
                if (!Equals(_cdGroup, value))
                {
                    _cdGroup = value;
                    AddModifiedProperty(nameof(CdGroup));
                }
            }
        }

        [NonSerialized] private int? _cdStGroup;
        [ColumnName("CD_ST_GROUP")]
        public int? CdStGroup
        {
            get => _cdStGroup;
            set
            {
                if (!Equals(_cdStGroup, value))
                {
                    _cdStGroup = value;
                    AddModifiedProperty(nameof(CdStGroup));
                }
            }
        }

        [NonSerialized] private int _cdConveyor;
        [ColumnName("CD_CONVEYOR")]
        [Key]
        public int CdConveyor
        {
            get => _cdConveyor;
            set
            {
                if (!Equals(_cdConveyor, value))
                {
                    _cdConveyor = value;
                    AddModifiedProperty(nameof(CdConveyor));
                }
            }
        }

        [NonSerialized] private int? _flAutoWarmstart;
        [ColumnName("FL_AUTO_WARMSTART")]
        public int? FlAutoWarmstart
        {
            get => _flAutoWarmstart;
            set
            {
                if (!Equals(_flAutoWarmstart, value))
                {
                    _flAutoWarmstart = value;
                    AddModifiedProperty(nameof(FlAutoWarmstart));
                }
            }
        }

        [NonSerialized] private int? _cdCode;
        [ColumnName("CD_CODE")]
        public int? CdCode
        {
            get => _cdCode;
            set
            {
                if (!Equals(_cdCode, value))
                {
                    _cdCode = value;
                    AddModifiedProperty(nameof(CdCode));
                }
            }
        }

    }
}
