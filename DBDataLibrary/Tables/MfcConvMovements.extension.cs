using System;
using System.ComponentModel.DataAnnotations;
using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;

namespace DBDataLibrary.Tables
{
    [TableType(TableTypes.Insertable | TableTypes.Updatable | TableTypes.Deletable)]
    public partial class MfcConvMovements
    {
    }
}
