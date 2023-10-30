using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEstadisticaDA
    {
        Models.General.ResponseGeneric<Models.Estadistica.Estadistica> addValue(Models.Estadistica.Venta venta);

        Models.General.ResponseGeneric<Models.Estadistica.Estadistica> searchValue(string valor, Models.Enums.EnumSearchEstadistica tipo);

        Models.General.ResponseGeneric<Models.Estadistica.Estadistica> getAll();
    }
}
