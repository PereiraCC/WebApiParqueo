using System;
using Models.Empleados;
using Models.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess.Class
{
    public class EmpleadosDA : IEmpleadosDA
    {
        private ProyectoParqueoContext _parqueoEntity;

        public EmpleadosDA(IOptions<ConfiguracionParqueo> options)
        {
            _parqueoEntity = new ProyectoParqueoContext(options.Value.ConnectionParqueo);
        }

        public ResponseGeneric<List<Models.Empleados.Empleados>> addValue(Empleados empleado)
        {
            try
            {
                // Se crea el metodo de empleado de Entity
                Models.DTOs.Empleado newEmpleado = new Models.DTOs.Empleado()
                {
                    Idparqueo = empleado.idParqueo,
                    NumeroEmpleado = empleado.NumeroEmpleado,
                    FechaIngreso = empleado.FechaIngreso,
                    PrimerNombre = empleado.PrimerNombre,
                    SegundoNombre = empleado.SegundoNombre,
                    PrimerApellido = empleado.PrimerApellido,
                    SegundoApellido = empleado.SegundoApellido,
                    FechaNacimiento = empleado.FechaNacimiento,
                    Identificacion = empleado.Identificacion,
                    Direccion = empleado.Direccion,
                    CorreoElectronico = empleado.CorreoElectronico,
                    Telefono = empleado.Telefono,
                    PersonaContacto = empleado.PersonaContacto
                };

                // Se guarda el registro
                _parqueoEntity.Empleados.Add(newEmpleado);

                // Se valida que se guarda correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>(formatEmpleados(_parqueoEntity.Empleados.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>("Error en el guardar un empleado");
                }

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
                // Se obtiene el empleado a eliminar
                Models.DTOs.Empleado findEmpleado = _parqueoEntity.Empleados.Find(idEmpleado);

                // Se elimina el empleado
                _parqueoEntity.Entry(findEmpleado).State = EntityState.Deleted;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>(formatEmpleados(_parqueoEntity.Empleados.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>("Error en el eliminar un empleado");
                }
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
                //Se obtiene el empleado a editar
                Models.DTOs.Empleado findEmpleado = _parqueoEntity.Empleados.Find(idEmpleado);

                // Se edita con los nuevo valores
                findEmpleado.Idparqueo = empleado.idParqueo;
                findEmpleado.NumeroEmpleado = empleado.NumeroEmpleado;
                findEmpleado.FechaIngreso = empleado.FechaIngreso;
                findEmpleado.PrimerNombre = empleado.PrimerNombre;
                findEmpleado.SegundoNombre = empleado.SegundoNombre;
                findEmpleado.PrimerApellido = empleado.PrimerApellido;
                findEmpleado.SegundoApellido = empleado.SegundoApellido;
                findEmpleado.FechaNacimiento = empleado.FechaNacimiento;
                findEmpleado.Identificacion = empleado.Identificacion;
                findEmpleado.Direccion = empleado.Direccion;
                findEmpleado.CorreoElectronico = empleado.CorreoElectronico;
                findEmpleado.Telefono = empleado.Telefono;
                findEmpleado.PersonaContacto = empleado.PersonaContacto;

                // Se edita el registro
                _parqueoEntity.Entry(findEmpleado).State = EntityState.Modified;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>(formatEmpleados(_parqueoEntity.Empleados.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Empleados.Empleados>>("Error en el editar un empleado");
                }

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
                List<Models.DTOs.Empleado> searchEmpleados = new List<Models.DTOs.Empleado>();
                switch (tipo)
                {
                    case Models.Enums.EnumSearchEmpleados.Numero:
                        searchEmpleados = _parqueoEntity.Empleados.Where(empleado => empleado.NumeroEmpleado.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEmpleados.Nombre:
                        searchEmpleados = _parqueoEntity.Empleados.Where(empleado => empleado.PrimerNombre.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEmpleados.Identificacion:
                        searchEmpleados = _parqueoEntity.Empleados.Where(empleado => empleado.Identificacion.Contains(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Empleados>>(formatEmpleados(searchEmpleados));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Empleados.Empleados>> getAll()
        {
            try
            {
                return new ResponseGeneric<List<Models.Empleados.Empleados>>(formatEmpleados(_parqueoEntity.Empleados.ToList()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Models.Empleados.Empleados> formatEmpleados(List<Models.DTOs.Empleado> empleados)
        {
            try
            {
                List<Models.Empleados.Empleados> allEmpleados = new List<Models.Empleados.Empleados>();

                foreach(Models.DTOs.Empleado empleado in empleados)
                {
                    allEmpleados.Add(new Models.Empleados.Empleados()
                    {
                        IdEmpleado = empleado.Idempleado,
                        idParqueo = empleado.Idparqueo,
                        NumeroEmpleado = empleado.NumeroEmpleado,
                        FechaIngreso = empleado.FechaIngreso ?? DateTime.Now,
                        PrimerNombre = empleado.PrimerNombre,
                        SegundoNombre = empleado.SegundoNombre,
                        PrimerApellido = empleado.PrimerApellido,
                        SegundoApellido = empleado.SegundoApellido,
                        FechaNacimiento = empleado.FechaNacimiento ?? DateTime.Now,
                        Identificacion = empleado.Identificacion,
                        Direccion = empleado.Direccion,
                        CorreoElectronico = empleado.CorreoElectronico,
                        Telefono = empleado.Telefono,
                        PersonaContacto = empleado.PersonaContacto

                    });
                }

                return allEmpleados;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
