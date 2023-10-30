using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.General;
using Models.Estadistica;

namespace WebApiParqueo.Controllers
{
    [ApiController]
    public class EstadisticaController : Controller
    {
        public BusinessLogic.Interfaces.IEstadisticaBL _estadisticaBL;

        public EstadisticaController(BusinessLogic.Interfaces.IEstadisticaBL estadisticaBL)
        {
            this._estadisticaBL = estadisticaBL;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<Estadistica> Create([FromBody] Venta venta)
        {
            try
            {
                return _estadisticaBL.addValue(venta);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Estadistica>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<Estadistica> Search(string valor, string tipo)
        {
            try
            {
                Models.Enums.EnumSearchEstadistica enumSearchEstadistica = Models.Enums.EnumSearchEstadistica.Mes;
                switch (tipo)
                {
                    case "1":
                        enumSearchEstadistica = Models.Enums.EnumSearchEstadistica.Mes;
                        break;

                    case "2":
                        enumSearchEstadistica = Models.Enums.EnumSearchEstadistica.Dia;
                        break;

                    case "3":
                        enumSearchEstadistica = Models.Enums.EnumSearchEstadistica.Tiempo;
                        break;

                    case "4":
                        enumSearchEstadistica = Models.Enums.EnumSearchEstadistica.ParqueosVendeMas;
                        break;
                }

                return _estadisticaBL.searchValue(valor, enumSearchEstadistica);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Estadistica>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<Estadistica> GetAll()
        {
            try
            {
                return _estadisticaBL.getAll();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Estadistica>(ex);
            }
        }

    }
}
