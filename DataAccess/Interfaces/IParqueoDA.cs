using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IParqueoDA
    {
        Models.General.ResponseGeneric<List<Models.Parqueos.Parqueo>> addValue(Models.Parqueos.Parqueo parqueo);

        Models.General.ResponseGeneric<List<Models.Parqueos.Parqueo>> editValue(Models.Parqueos.Parqueo parqueo, int idParqueo);

        Models.General.ResponseGeneric<List<Models.Parqueos.Parqueo>> searchValue(string valor, Models.Enums.EnumSearchParqueo tipo);

        Models.General.ResponseGeneric<List<Models.Parqueos.Parqueo>> deleteValue(int idParqueo);

        Models.General.ResponseGeneric<List<Models.Parqueos.Parqueo>> getAll();
    }
}
