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
    public partial class MfcConvUdcType_data: IDBData
    {
        [ColumnName("OID")]
        public long? Oid { get; set; }
        [ColumnName("OID_PROPERTY")]
        public long? OidProperty { get; set; }
        [ColumnName("TP_UDC")]
        public int? TpUdc { get; set; }
        [ColumnName("ID_UDC")]
        public int? IdUdc { get; set; }
        [ColumnName("DSC_UDC")]
        public string DscUdc { get; set; }
        [ColumnName("WIDTH")]
        public int? Width { get; set; }
        [ColumnName("LENGTH")]
        public int? Length { get; set; }
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
        [ColumnName("TP_UDC_HOST")]
        public string TpUdcHost { get; set; }
        [ColumnName("DSC_UDC_RF")]
        public string DscUdcRf { get; set; }
        [ColumnName("TP_UDC_PLC")]
        public int? TpUdcPlc { get; set; }
        [ColumnName("CD_LANGUAGE")]
        public string CdLanguage { get; set; }
    }
}
