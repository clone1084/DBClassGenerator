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
    public partial class MfcConvUdmType_data: IDBData
    {
        [ColumnName("OID")]
        [Key]
        public long Oid { get; set; }
        [ColumnName("OID_PROPERTY")]
        public long? OidProperty { get; set; }
        [ColumnName("TP_UDM")]
        [Required]
        public int TpUdm { get; set; } = default(int);
        [ColumnName("ID_UDM")]
        [Required]
        public string IdUdm { get; set; } = "";
        [ColumnName("DSC_UDM")]
        [Required]
        public string DscUdm { get; set; } = "";
        [ColumnName("WIDTH")]
        [Required]
        public int Width { get; set; } = default(int);
        [ColumnName("LENGTH")]
        [Required]
        public int Length { get; set; } = default(int);
        [ColumnName("WEIGHT")]
        public int? Weight { get; set; }
        [ColumnName("LEN_X")]
        public int? LenX { get; set; }
        [ColumnName("LEN_Y")]
        public int? LenY { get; set; }
        [ColumnName("LEN_Z")]
        public int? LenZ { get; set; }
        [ColumnName("MAX_WEIGHT")]
        public int? MaxWeight { get; set; }
        [ColumnName("TARE")]
        public int? Tare { get; set; }
        [ColumnName("DOUBLE_DEPTH")]
        public int? DoubleDepth { get; set; }
        [ColumnName("TP_UDM_HOST")]
        public string TpUdmHost { get; set; }
        [ColumnName("DSC_UDM_RF")]
        public string DscUdmRf { get; set; }
        [ColumnName("TP_UDM_PLC")]
        public int? TpUdmPlc { get; set; }
        [ColumnName("CD_LANGUAGE")]
        public string CdLanguage { get; set; }
        [ColumnName("HEIGHT")]
        public int? Height { get; set; }
    }
}
