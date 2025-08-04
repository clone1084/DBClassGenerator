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
    [TableName("MFC_CONV_SPECIAL_CROSS")]
    public partial class MfcConvSpecialCross : ACrudBase<MfcConvSpecialCross>
    {
        public MfcConvSpecialCross() : base() { }
        
        [NonSerialized] private string _cdCross = "";
        [ColumnName("CD_CROSS")]
        [Required]
        public string CdCross
        {
            get => _cdCross;
            set
            {
                if (!Equals(_cdCross, value))
                {
                    _cdCross = value;
                    AddModifiedProperty(nameof(CdCross));
                }
            }
        }

        [NonSerialized] private string _first = "";
        [ColumnName("FIRST")]
        [Required]
        public string First
        {
            get => _first;
            set
            {
                if (!Equals(_first, value))
                {
                    _first = value;
                    AddModifiedProperty(nameof(First));
                }
            }
        }

        [NonSerialized] private string _second = "";
        [ColumnName("SECOND")]
        [Required]
        public string Second
        {
            get => _second;
            set
            {
                if (!Equals(_second, value))
                {
                    _second = value;
                    AddModifiedProperty(nameof(Second));
                }
            }
        }

        [NonSerialized] private int? _tot;
        [ColumnName("TOT")]
        public int? Tot
        {
            get => _tot;
            set
            {
                if (!Equals(_tot, value))
                {
                    _tot = value;
                    AddModifiedProperty(nameof(Tot));
                }
            }
        }

        [NonSerialized] private int? _counter;
        [ColumnName("COUNTER")]
        public int? Counter
        {
            get => _counter;
            set
            {
                if (!Equals(_counter, value))
                {
                    _counter = value;
                    AddModifiedProperty(nameof(Counter));
                }
            }
        }

    }
}
