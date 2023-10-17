using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IEmpleadosBL
    {
        Models.General.ResponseGeneric<List<Models.Empleados.Empleados>> addValue(Models.Empleados.Empleados empleado);

        Models.General.ResponseGeneric<List<Models.Empleados.Empleados>> editValue(Models.Empleados.Empleados empleado, int idEmpleado);

        Models.General.ResponseGeneric<List<Models.Empleados.Empleados>> searchValue(string valor, Models.Enums.EnumSearchEmpleados tipo);

        Models.General.ResponseGeneric<List<Models.Empleados.Empleados>> deleteValue(int idEmpleado);
    }
}
