using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.General;
using Models.Parqueos;

namespace WebApiParqueo.Controllers
{
    [ApiController]
    public class ParqueoController : Controller
    {
        public BusinessLogic.Interfaces.IParqueoBL _parqueoBL;

        public ParqueoController(BusinessLogic.Interfaces.IParqueoBL parqueoBL)
        {
            this._parqueoBL = parqueoBL;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Parqueo>> Create([FromBody] Parqueo parqueo)
        {
            try
            {
                return _parqueoBL.addValue(parqueo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Parqueo>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Parqueo>> Edit([FromBody] Parqueo parqueo)
        {
            try
            {
                return _parqueoBL.editValue(parqueo, parqueo.idParqueo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Parqueo>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Parqueo>> Search(string valor, string tipo)
        {
            try
            {
                Models.Enums.EnumSearchParqueo enumSearchParqueo = Models.Enums.EnumSearchParqueo.Nombre;
                switch (tipo)
                {
                    case "1":
                        enumSearchParqueo = Models.Enums.EnumSearchParqueo.Nombre;
                        break;

                    case "2":
                        enumSearchParqueo = Models.Enums.EnumSearchParqueo.CantididadVehiculos;
                        break;

                    case "3":
                        enumSearchParqueo = Models.Enums.EnumSearchParqueo.Tarifa;
                        break;
                }

                return _parqueoBL.searchValue(valor, enumSearchParqueo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Parqueo>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Parqueo>> Delete(int idParqueo)
        {
            try
            {
                return _parqueoBL.deleteValue(idParqueo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Parqueo>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Parqueo>> GetAll()
        {
            try
            {
                return _parqueoBL.getAll();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Parqueo>>(ex);
            }
        }
    }
}
