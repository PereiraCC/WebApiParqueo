using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITiqueteBL
    {
        Models.General.ResponseGeneric<List<Models.Tiquetes.Tiquete>> addValue(Models.Tiquetes.Tiquete tiquete);

        Models.General.ResponseGeneric<List<Models.Tiquetes.Tiquete>> editValue(Models.Tiquetes.Tiquete tiquete, int idTiquete);

        Models.General.ResponseGeneric<List<Models.Tiquetes.Tiquete>> searchValue(string valor, Models.Enums.EnumSearchTiquete tipo);

        Models.General.ResponseGeneric<List<Models.Tiquetes.Tiquete>> deleteValue(int idTiquete);
    }
}
