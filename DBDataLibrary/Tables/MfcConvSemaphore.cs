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
    [TableName("MFC_CONV_SEMAPHORE")]
    public partial class MfcConvSemaphore : ACrudBase<MfcConvSemaphore>
    {
        public MfcConvSemaphore() : base() { }
        
        [NonSerialized] private int _levelItem = default(int);
        [ColumnName("LEVEL_ITEM")]
        [Required]
        public int LevelItem
        {
            get => _levelItem;
            set
            {
                if (!Equals(_levelItem, value))
                {
                    _levelItem = value;
                    AddModifiedProperty(nameof(LevelItem));
                }
            }
        }

        [NonSerialized] private int _cdItem = default(int);
        [ColumnName("CD_ITEM")]
        [Required]
        public int CdItem
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

    }
}
