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
    public partial class MfcConvMultifFormData_data: IDBData
    {
        [ColumnName("OID_UDM")]
        public long? OidUdm { get; set; }
        [ColumnName("CD_UDC")]
        public string CdUdc { get; set; }
        [ColumnName("CD_UDM")]
        public string CdUdm { get; set; }
        [ColumnName("FL_READER")]
        public string FlReader { get; set; }
        [ColumnName("COD_SCARTO")]
        public int? CodScarto { get; set; }
        [ColumnName("DESCR_SCARTO")]
        public string DescrScarto { get; set; }
        [ColumnName("DT_INSERT")]
        public DateTime? DtInsert { get; set; }
        [ColumnName("CD_ARTICLE")]
        public string CdArticle { get; set; }
        [ColumnName("DSC_ARTICLE")]
        public string DscArticle { get; set; }
        [ColumnName("OID_ORDER")]
        public long? OidOrder { get; set; }
        [ColumnName("OID_ORDER_R")]
        public long? OidOrderR { get; set; }
        [ColumnName("UDC_QTY")]
        public long? UdcQty { get; set; }
        [ColumnName("UDC_SPARE")]
        public int? UdcSpare { get; set; }
        [ColumnName("CD_ORDER")]
        public string CdOrder { get; set; }
        [ColumnName("IS_TO_LIGHT")]
        public int? IsToLight { get; set; }
        [ColumnName("QTY_TO_PICK")]
        public long? QtyToPick { get; set; }
        [ColumnName("FL_TAKEN")]
        public int? FlTaken { get; set; }
        [ColumnName("CD_ITEM_FIFO")]
        public string CdItemFifo { get; set; }
        [ColumnName("FL_PICKED")]
        public int? FlPicked { get; set; }
        [ColumnName("DT_PICKING")]
        public DateTime? DtPicking { get; set; }
    }
}
