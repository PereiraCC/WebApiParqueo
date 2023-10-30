using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.General;
using Models.Tiquetes;

namespace WebApiParqueo.Controllers
{
    [ApiController]
    public class TiquetesController : Controller
    {
        public BusinessLogic.Interfaces.ITiqueteBL _tiqueteBL;

        public TiquetesController(BusinessLogic.Interfaces.ITiqueteBL tiqueteBL)
        {
            this._tiqueteBL = tiqueteBL;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Tiquete>> Create([FromBody] Tiquete tiquete)
        {
            try
            {
                return _tiqueteBL.addValue(tiquete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Tiquete>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Tiquete>> Edit([FromBody] Tiquete tiquete)
        {
            try
            {
                return _tiqueteBL.editValue(tiquete, tiquete.idTiquete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Tiquete>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Tiquete>> Search(string valor, string tipo)
        {
            try
            {
                Models.Enums.EnumSearchTiquete enumSearchTiquete = Models.Enums.EnumSearchTiquete.Placa;
                switch (tipo)
                {
                    case "1":
                        enumSearchTiquete = Models.Enums.EnumSearchTiquete.Placa;
                        break;

                    case "2":
                        enumSearchTiquete = Models.Enums.EnumSearchTiquete.Numero;
                        break;

                    case "3":
                        enumSearchTiquete = Models.Enums.EnumSearchTiquete.Parqueo;
                        break;

                    case "4":
                        enumSearchTiquete = Models.Enums.EnumSearchTiquete.Empleado;
                        break;
                }

                return _tiqueteBL.searchValue(valor, enumSearchTiquete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Tiquete>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Tiquete>> Delete(int idTiquete)
        {
            try
            {
                return _tiqueteBL.deleteValue(idTiquete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Tiquete>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Tiquete>> GetAll()
        {
            try
            {
                return _tiqueteBL.getAll();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Tiquete>>(ex);
            }
        }
    }
}
