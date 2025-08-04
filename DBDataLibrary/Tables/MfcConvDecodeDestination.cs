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
    [TableName("MFC_CONV_DECODE_DESTINATION")]
    public partial class MfcConvDecodeDestination : ACrudBase<MfcConvDecodeDestination>
    {
        public MfcConvDecodeDestination() : base() { }
        
        [NonSerialized] private int _destType;
        [ColumnName("DEST_TYPE")]
        [Key]
        public int DestType
        {
            get => _destType;
            set
            {
                if (!Equals(_destType, value))
                {
                    _destType = value;
                    AddModifiedProperty(nameof(DestType));
                }
            }
        }

        [NonSerialized] private int _destPar;
        [ColumnName("DEST_PAR")]
        [Key]
        public int DestPar
        {
            get => _destPar;
            set
            {
                if (!Equals(_destPar, value))
                {
                    _destPar = value;
                    AddModifiedProperty(nameof(DestPar));
                }
            }
        }

        [NonSerialized] private string _cdItem;
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

    }
}
