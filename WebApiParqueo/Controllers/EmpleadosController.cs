using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.General;
using Models.Empleados;

namespace WebApiParqueo.Controllers
{
    [ApiController]
    public class EmpleadosController : Controller
    {
        public BusinessLogic.Interfaces.IEmpleadosBL _empleadosBL;

        public EmpleadosController(BusinessLogic.Interfaces.IEmpleadosBL empleadosBL)
        {
            this._empleadosBL = empleadosBL;
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Empleados>> Create([FromBody] Empleados empleado)
        {
            try
            {
                return _empleadosBL.addValue(empleado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Empleados>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Empleados>> Edit([FromBody] Empleados empleado)
        {
            try
            {
                return _empleadosBL.editValue(empleado, empleado.IdEmpleado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Empleados>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Empleados>> Search(string valor, string tipo)
        {
            try
            {
                Models.Enums.EnumSearchEmpleados enumSearchEmpleados = Models.Enums.EnumSearchEmpleados.Nombre;
                switch (tipo)
                {
                    case "1":
                        enumSearchEmpleados = Models.Enums.EnumSearchEmpleados.Numero;
                        break;

                    case "2":
                        enumSearchEmpleados = Models.Enums.EnumSearchEmpleados.Nombre;
                        break;

                    case "3":
                        enumSearchEmpleados = Models.Enums.EnumSearchEmpleados.Identificacion;
                        break;
                }

                return _empleadosBL.searchValue(valor, enumSearchEmpleados);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Empleados>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Empleados>> Delete(int idEmpleado)
        {
            try
            {
                return _empleadosBL.deleteValue(idEmpleado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Empleados>>(ex);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public ResponseGeneric<List<Empleados>> GetAll()
        {
            try
            {
                return _empleadosBL.getAll();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Empleados>>(ex);
            }
        }
    }
}
