using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.Empleados;
using Models.Enums;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Class
{
    public class EmpleadosBL : IEmpleadosBL
    {
        private readonly IEmpleadosDA _Empleados;
        
        public EmpleadosBL(IEmpleadosDA Empleados)
        {
            _Empleados = Empleados;
        }

        public ResponseGeneric<List<Empleados>> addValue(Empleados empleado)
        {
            return _Empleados.addValue(empleado);
        }

        public ResponseGeneric<List<Empleados>> deleteValue(int idEmpleado)
        {
            return _Empleados.deleteValue(idEmpleado);
        }

        public ResponseGeneric<List<Empleados>> editValue(Empleados empleado, int idEmpleado)
        {
            return _Empleados.editValue(empleado, idEmpleado);
        }

        public ResponseGeneric<List<Empleados>> searchValue(string valor, EnumSearchEmpleados tipo)
        {
            return _Empleados.searchValue(valor, tipo);
        }
    }
}
