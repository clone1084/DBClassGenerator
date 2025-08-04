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
    [TableName("MFC_CONV_PROFILE_CHECK_ERROR")]
    public partial class MfcConvProfileCheckError : ACrudBase<MfcConvProfileCheckError>
    {
        public MfcConvProfileCheckError() : base() { }
        
        [NonSerialized] private int _cdError = default(int);
        [ColumnName("CD_ERROR")]
        [Required]
        public int CdError
        {
            get => _cdError;
            set
            {
                if (!Equals(_cdError, value))
                {
                    _cdError = value;
                    AddModifiedProperty(nameof(CdError));
                }
            }
        }

        [NonSerialized] private string _dscError = "";
        [ColumnName("DSC_ERROR")]
        [Required]
        public string DscError
        {
            get => _dscError;
            set
            {
                if (!Equals(_dscError, value))
                {
                    _dscError = value;
                    AddModifiedProperty(nameof(DscError));
                }
            }
        }

    }
}
