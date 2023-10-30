using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.Tiquetes;
using Models.Enums;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Estadistica;

namespace BusinessLogic.Class
{
    public class EstadisticaBL : IEstadisticaBL
    {
        private readonly IEstadisticaDA _Estadistica;
        
        public EstadisticaBL(IEstadisticaDA Estadistica)
        {
            _Estadistica = Estadistica;
        }

        public ResponseGeneric<Estadistica> addValue(Venta venta)
        {
            return _Estadistica.addValue(venta);
        }

        public ResponseGeneric<Estadistica> searchValue(string valor, EnumSearchEstadistica tipo)
        {
            return _Estadistica.searchValue(valor, tipo);
        }

        public ResponseGeneric<Estadistica> getAll()
        {
            return _Estadistica.getAll();
        }
    }
}
