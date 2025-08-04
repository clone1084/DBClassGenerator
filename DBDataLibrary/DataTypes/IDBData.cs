using DBDataLibrary.Attributes;
using DBDataLibrary.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataLibrary.DataTypes
{
    public interface IDBData
    {
        void SetCrudClass(ICrudClass aCrudBase);
    }
}
