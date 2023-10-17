using System;
using Models.Empleados;
using Models.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Class
{
    public class EmpleadosDA : IEmpleadosDA
    {
        public EmpleadosDA()
        {
        }

        public ResponseGeneric<List<Empleados>> addValue(Empleados empleado)
        {
            try
            {
                // Se agrega el nuevo empleado
                GlobalVariables.Empleados.Add(empleado);

                return new ResponseGeneric<List<Empleados>>(GlobalVariables.Empleados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Empleados>> deleteValue(int idEmpleado)
        {
            try
            {
                // Se busca el index de tiquete a eliminar
                int indexEmpleado = GlobalVariables.Empleados.FindIndex(emple => emple.IdEmpleado == idEmpleado);

                if (indexEmpleado == -1)
                {
                    return new ResponseGeneric<List<Empleados>>("No se encontro el empleado");
                }

                // Se eliminar el tiquete
                Models.General.GlobalVariables.Empleados.RemoveAt(indexEmpleado);

                return new ResponseGeneric<List<Empleados>>(GlobalVariables.Empleados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Empleados>> editValue(Empleados empleado, int idEmpleado)
        {
            try
            {
                // Se busca el index de tiquete a modificar
                int indexEmpleado = GlobalVariables.Empleados.FindIndex(emple => emple.IdEmpleado == idEmpleado);

                // Se valida que index sea correcto
                if (indexEmpleado != -1)
                {
                    // Se modifica el objeto
                    GlobalVariables.Empleados[indexEmpleado].idParqueo = empleado.idParqueo;
                    GlobalVariables.Empleados[indexEmpleado].NumeroEmpleado = empleado.NumeroEmpleado;
                    GlobalVariables.Empleados[indexEmpleado].FechaIngreso = empleado.FechaIngreso;
                    GlobalVariables.Empleados[indexEmpleado].PrimerNombre = empleado.PrimerNombre;
                    GlobalVariables.Empleados[indexEmpleado].SegundoNombre = empleado.SegundoNombre;
                    GlobalVariables.Empleados[indexEmpleado].PrimerApellido = empleado.PrimerApellido;
                    GlobalVariables.Empleados[indexEmpleado].SegundoApellido = empleado.SegundoApellido;
                    GlobalVariables.Empleados[indexEmpleado].FechaNacimiento = empleado.FechaNacimiento;
                    GlobalVariables.Empleados[indexEmpleado].Identificacion = empleado.Identificacion;
                    GlobalVariables.Empleados[indexEmpleado].Direccion = empleado.Direccion;
                    GlobalVariables.Empleados[indexEmpleado].CorreoElectronico = empleado.CorreoElectronico;
                    GlobalVariables.Empleados[indexEmpleado].Telefono = empleado.Telefono;
                    GlobalVariables.Empleados[indexEmpleado].PersonaContacto = empleado.PersonaContacto;
                }

                return new ResponseGeneric<List<Empleados>>(GlobalVariables.Empleados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Empleados>> searchValue(string valor, Models.Enums.EnumSearchEmpleados tipo)
        {
            try
            {
                switch (tipo)
                {
                    case Models.Enums.EnumSearchEmpleados.Numero:
                        GlobalVariables.EmpleadosFiltrados = GlobalVariables.Empleados.Where(empleado => empleado.NumeroEmpleado.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEmpleados.Nombre:
                        GlobalVariables.EmpleadosFiltrados = GlobalVariables.Empleados.Where(empleado => empleado.PrimerNombre.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEmpleados.Identificacion:
                        GlobalVariables.EmpleadosFiltrados = GlobalVariables.Empleados.Where(empleado => empleado.Identificacion.Contains(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Empleados>>(GlobalVariables.EmpleadosFiltrados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
