using System;
using Models.Tiquetes;
using Models.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Models.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Class
{
    public class TiqueteDA : ITiqueteDA
    {
        private ProyectoParqueoContext _parqueoEntity;

        public TiqueteDA(IOptions<ConfiguracionParqueo> options)
        {
            _parqueoEntity = new ProyectoParqueoContext(options.Value.ConnectionParqueo);
        }

        public ResponseGeneric<List<Models.Tiquetes.Tiquete>> addValue(Models.Tiquetes.Tiquete tiquete)
        {
            try
            {
                Models.DTOs.Parqueo seletedParqueo = _parqueoEntity.Parqueos.Find(tiquete.idParqueo);
                Models.DTOs.Empleado seletedEmpleado = _parqueoEntity.Empleados.Find(tiquete.idEmpleado);

                // Se crea el modelo de Tiquete de Entity
                Models.DTOs.Tiquete newTiquete = new Models.DTOs.Tiquete()
                {
                    Idparqueo = tiquete.idParqueo,
                    NombreParqueo = (seletedParqueo != null) ? seletedParqueo.Nombre : "",
                    Idempleado = tiquete.idEmpleado,
                    NombreEmpleado = (seletedEmpleado == null) ? "" : seletedEmpleado.PrimerNombre + " " + seletedEmpleado.PrimerApellido,
                    FechaIngreso = tiquete.fechaIngreso,
                    FechaSalida = null,
                    MontoPagar = null,
                    Placa = tiquete.placa,
                    TiempoConsumido = null,
                    Venta = false
                };

                // Se guarda el registro
                _parqueoEntity.Tiquetes.Add(newTiquete);

                // Se valida que se guarda correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>(formatTiquetes(_parqueoEntity.Tiquetes.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>("Error en el guardar un tiquete");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Tiquetes.Tiquete>> deleteValue(int idTiquete)
        {
            try
            {
                // Se obtiene el parqueo a eliminar
                Models.DTOs.Tiquete findTiquete = _parqueoEntity.Tiquetes.Find(idTiquete);

                // Se elimina el empleado
                _parqueoEntity.Entry(findTiquete).State = EntityState.Deleted;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>(formatTiquetes(_parqueoEntity.Tiquetes.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>("Error en el eliminar un tiquete");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Tiquetes.Tiquete>> editValue(Models.Tiquetes.Tiquete tiquete, int idTiquete)
        {
            try
            {
                //Se obtiene el empleado a editar
                Models.DTOs.Tiquete findTiquete = _parqueoEntity.Tiquetes.Find(idTiquete);

                // Se edita con los nuevo valores
                findTiquete.FechaSalida = tiquete.fechaSalida;
                findTiquete.MontoPagar = (tiquete.montoPagar != null) ? tiquete.montoPagar.ToString() : "0";
                findTiquete.TiempoConsumido = tiquete.tiempoConsumido;
                findTiquete.Venta = true;

                // Se edita el registro
                _parqueoEntity.Entry(findTiquete).State = EntityState.Modified;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>(formatTiquetes(_parqueoEntity.Tiquetes.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>("Error en el editar un tiquete");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Tiquetes.Tiquete>> searchValue(string valor, Models.Enums.EnumSearchTiquete tipo)
        {
            try
            {
                List<Models.DTOs.Tiquete> searchTiquete = new List<Models.DTOs.Tiquete>();
                switch (tipo)
                {
                    case Models.Enums.EnumSearchTiquete.Placa:
                        searchTiquete = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Placa.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Numero:
                        searchTiquete = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Idtiquete == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Parqueo:
                        searchTiquete = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Idparqueo == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Empleado:
                        searchTiquete = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Idempleado == Int32.Parse(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>(formatTiquetes(searchTiquete));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Tiquetes.Tiquete>> getAll()
        {
            try
            {
                return new ResponseGeneric<List<Models.Tiquetes.Tiquete>>(formatTiquetes(_parqueoEntity.Tiquetes.ToList()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Models.Tiquetes.Tiquete> formatTiquetes(List<Models.DTOs.Tiquete> tiquetes)
        {
            List<Models.Tiquetes.Tiquete> allTiquetes = new List<Models.Tiquetes.Tiquete>();

            foreach (Models.DTOs.Tiquete tiquete in tiquetes)
            {
                Models.DTOs.Parqueo seletedParqueo = _parqueoEntity.Parqueos.Find(tiquete.Idparqueo);
                Models.DTOs.Empleado seletedEmpleado = _parqueoEntity.Empleados.Find(tiquete.Idempleado);

                allTiquetes.Add(new Models.Tiquetes.Tiquete() { 
                    idTiquete = tiquete.Idtiquete,
                    idParqueo = tiquete.Idparqueo,
                    nombreParqueo = (seletedParqueo != null) ? seletedParqueo.Nombre : "",
                    idEmpleado = tiquete.Idempleado,
                    nombreEmpleado = (seletedEmpleado == null) ? "" : seletedEmpleado.PrimerNombre + " " + seletedEmpleado.PrimerApellido,
                    fechaIngreso = tiquete.FechaIngreso ?? DateTime.Now,
                    fechaSalida = tiquete.FechaSalida ?? DateTime.Now,
                    montoPagar = (tiquete.MontoPagar != null) ? float.Parse(tiquete.MontoPagar) : 0,
                    placa = tiquete.Placa,
                    tiempoConsumido = tiquete.TiempoConsumido,
                    venta = tiquete.Venta
                });
            }

            return allTiquetes;
        }

    }
}
