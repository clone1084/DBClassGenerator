using System;
using System.ComponentModel.DataAnnotations;
using DBDataLibrary.Attributes;
using DBDataLibrary.DataTypes;

namespace DBDataLibrary.Tables
{
    //  --------------------------------------------------
    // --            AUTOMATIC GENERATED CLASS           --
    // --                DO NOT MODIFY!!!                --
    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --
    //  --------------------------------------------------
    public partial class MfcConvError_data: IDBData
    {
        [ColumnName("CD_ERROR")]
        public int? CdError { get; set; }
        [ColumnName("CD_CONVEYOR")]
        public int? CdConveyor { get; set; }
        [ColumnName("CD_LANGUAGE")]
        public string CdLanguage { get; set; }
        [ColumnName("DSC_ERROR")]
        public string DscError { get; set; }
        [ColumnName("OID_MFC_DATABLOCK")]
        public long? OidMfcDatablock { get; set; }
        [ColumnName("BIT_POSITION")]
        public long? BitPosition { get; set; }
        [ColumnName("TP_ANOMALY")]
        public int? TpAnomaly { get; set; }
    }
}
