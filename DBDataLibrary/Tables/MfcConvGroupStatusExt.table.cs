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
    [TableName("MFC_CONV_GROUP_STATUS_EXT")]
    public partial class MfcConvGroupStatusExt : ACrudBase<MfcConvGroupStatusExt>
    {
        public MfcConvGroupStatusExt() : base() { }
        
        [NonSerialized] private string _cdGroup = "";
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

        [NonSerialized] private int _cdGrouping = default(int);
        [ColumnName("CD_GROUPING")]
        [Required]
        public int CdGrouping
        {
            get => _cdGrouping;
            set
            {
                if (!Equals(_cdGrouping, value))
                {
                    _cdGrouping = value;
                    AddModifiedProperty(nameof(CdGrouping));
                }
            }
        }

        [NonSerialized] private int? _priority;
        [ColumnName("PRIORITY")]
        public int? Priority
        {
            get => _priority;
            set
            {
                if (!Equals(_priority, value))
                {
                    _priority = value;
                    AddModifiedProperty(nameof(Priority));
                }
            }
        }

    }
}
