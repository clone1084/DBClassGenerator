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
    [TableName("MFC_CONV_MULTIFUNCTION")]
    public partial class MfcConvMultifunction : ACrudBase<MfcConvMultifunction>
    {
        public MfcConvMultifunction() : base() { }
        
        [NonSerialized] private string _cdUdm = "";
        [ColumnName("CD_UDM")]
        public string CdUdm
        {
            get => _cdUdm;
            set
            {
                if (!Equals(_cdUdm, value))
                {
                    _cdUdm = value;
                    AddModifiedProperty(nameof(CdUdm));
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
