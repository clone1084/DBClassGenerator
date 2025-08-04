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
    [TableName("MFC_CONV_LIFTS")]
    public partial class MfcConvLifts : ACrudBase<MfcConvLifts>
    {
        public MfcConvLifts() : base() { }
        
        [NonSerialized] private string _lift = "";
        [ColumnName("LIFT")]
        [Required]
        public string Lift
        {
            get => _lift;
            set
            {
                if (!Equals(_lift, value))
                {
                    _lift = value;
                    AddModifiedProperty(nameof(Lift));
                }
            }
        }

        [NonSerialized] private string _dsc;
        [ColumnName("DSC")]
        public string Dsc
        {
            get => _dsc;
            set
            {
                if (!Equals(_dsc, value))
                {
                    _dsc = value;
                    AddModifiedProperty(nameof(Dsc));
                }
            }
        }

        [NonSerialized] private int _available = default(int);
        [ColumnName("AVAILABLE")]
        [Required]
        public int Available
        {
            get => _available;
            set
            {
                if (!Equals(_available, value))
                {
                    _available = value;
                    AddModifiedProperty(nameof(Available));
                }
            }
        }

        [NonSerialized] private string _zi = "";
        [ColumnName("ZI")]
        [Required]
        public string Zi
        {
            get => _zi;
            set
            {
                if (!Equals(_zi, value))
                {
                    _zi = value;
                    AddModifiedProperty(nameof(Zi));
                }
            }
        }

        [NonSerialized] private int _count = default(int);
        [ColumnName("COUNT")]
        [Required]
        public int Count
        {
            get => _count;
            set
            {
                if (!Equals(_count, value))
                {
                    _count = value;
                    AddModifiedProperty(nameof(Count));
                }
            }
        }

        [NonSerialized] private int? _maxCount;
        [ColumnName("MAX_COUNT")]
        public int? MaxCount
        {
            get => _maxCount;
            set
            {
                if (!Equals(_maxCount, value))
                {
                    _maxCount = value;
                    AddModifiedProperty(nameof(MaxCount));
                }
            }
        }

    }
}
