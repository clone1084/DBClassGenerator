using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
namespace DBDataLibrary.Tables
{
    //  --------------------------------------------------
    // --            AUTOMATIC GENERATED CLASS           --
    // --                DO NOT MODIFY!!!                --
    // -- ANY CHANGE WILL BE LOST AT THE NEXT GENERATION --
    //  --------------------------------------------------

    [TableType(TableTypes.Insertable | TableTypes.Updatable | TableTypes.Deletable)]
    public partial class MfcConvMovements : ACrudBase<MfcConvMovements, MfcConvMovements_data>
    {
        public MfcConvMovements() : base() { }
        public override string TableName => "MFC_CONV_MOVEMENTS";
    }
}
