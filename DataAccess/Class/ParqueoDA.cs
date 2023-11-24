using System;
using Models.Empleados;
using Models.Parqueos;
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
    public class ParqueoDA : IParqueoDA
    {
        private ProyectoParqueoContext _parqueoEntity;

        public ParqueoDA(IOptions<ConfiguracionParqueo> options)
        {
            _parqueoEntity = new ProyectoParqueoContext(options.Value.ConnectionParqueo);
        }

        public ResponseGeneric<List<Models.Parqueos.Parqueo>> addValue(Models.Parqueos.Parqueo parqueo)
        {
            try
            {
                // Se crea el modelo de parqueo de Entity
                Models.DTOs.Parqueo newParqueo = new Models.DTOs.Parqueo()
                {
                    Nombre = parqueo.Nombre,
                    CantidadMaximaVehiculos = parqueo.CantidadMaximaVehiculos,
                    HoraApertura = parqueo.HoraApertura,
                    HoraCierre = parqueo.HoraCierre,
                    TarifaHora = parqueo.TarifaHora,
                    TarifaMediaHora = parqueo.TarifaMediaHora
                };

                // Se guarda el registro
                _parqueoEntity.Parqueos.Add(newParqueo);

                // Se valida que se guarda correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>(formatParqueos(_parqueoEntity.Parqueos.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>("Error en el guardar un parqueo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Parqueos.Parqueo>> deleteValue(int idParqueo)
        {
            try
            {
                // Se obtiene el parqueo a eliminar
                Models.DTOs.Parqueo findParqueo = _parqueoEntity.Parqueos.Find(idParqueo);

                // Se elimina el empleado
                _parqueoEntity.Entry(findParqueo).State = EntityState.Deleted;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>(formatParqueos(_parqueoEntity.Parqueos.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>("Error en el eliminar un parqueo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Parqueos.Parqueo>> editValue(Models.Parqueos.Parqueo parqueo, int idParqueo)
        {
            try
            {
                //Se obtiene el empleado a editar
                Models.DTOs.Parqueo findParqueo = _parqueoEntity.Parqueos.Find(idParqueo);

                // Se edita con los nuevo valores
                findParqueo.Nombre = parqueo.Nombre;
                findParqueo.CantidadMaximaVehiculos = parqueo.CantidadMaximaVehiculos;
                findParqueo.HoraApertura = parqueo.HoraApertura;
                findParqueo.HoraCierre = parqueo.HoraCierre;
                findParqueo.TarifaHora = parqueo.TarifaHora;
                findParqueo.TarifaMediaHora = parqueo.TarifaMediaHora;

                // Se edita el registro
                _parqueoEntity.Entry(findParqueo).State = EntityState.Modified;

                // Se valida que se edita correctamente
                if (_parqueoEntity.SaveChanges() == 1)
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>(formatParqueos(_parqueoEntity.Parqueos.ToList()));
                }
                else
                {
                    return new ResponseGeneric<List<Models.Parqueos.Parqueo>>("Error en el editar un parqueo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Parqueos.Parqueo>> searchValue(string valor, Models.Enums.EnumSearchParqueo tipo)
        {
            try
            {
                List<Models.DTOs.Parqueo> searchParqueos = new List<Models.DTOs.Parqueo>();
                switch (tipo)
                {
                    case Models.Enums.EnumSearchParqueo.Nombre:
                        searchParqueos = _parqueoEntity.Parqueos.Where(parqueo => parqueo.Nombre.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchParqueo.CantididadVehiculos:
                        searchParqueos = _parqueoEntity.Parqueos.Where(parqueo => parqueo.CantidadMaximaVehiculos == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchParqueo.Tarifa:
                        searchParqueos = _parqueoEntity.Parqueos.Where(parqueo => parqueo.TarifaHora == Int32.Parse(valor) || parqueo.TarifaMediaHora == Int32.Parse(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Models.Parqueos.Parqueo>>(formatParqueos(searchParqueos));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Models.Parqueos.Parqueo>> getAll()
        {
            try
            {
                return new ResponseGeneric<List<Models.Parqueos.Parqueo>>(formatParqueos(_parqueoEntity.Parqueos.ToList()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Models.Parqueos.Parqueo> formatParqueos(List<Models.DTOs.Parqueo> parqueos)
        {
            try
            {
                List<Models.Parqueos.Parqueo> allParqueos = new List<Models.Parqueos.Parqueo>();

                foreach (Models.DTOs.Parqueo parqueo in parqueos)
                {
                    allParqueos.Add(new Models.Parqueos.Parqueo()
                    {
                        idParqueo = parqueo.Idparqueo,
                        Nombre = parqueo.Nombre,
                        CantidadMaximaVehiculos = parqueo.CantidadMaximaVehiculos ?? 0,
                        HoraApertura = parqueo.HoraApertura ?? DateTime.Now,
                        HoraCierre = parqueo.HoraCierre ?? DateTime.Now,
                        TarifaHora = (parqueo.TarifaHora != null) ? (float)parqueo.TarifaHora : 0,
                        TarifaMediaHora = (parqueo.TarifaMediaHora != null) ? (float)parqueo.TarifaMediaHora : 0,
                    });
                }

                return allParqueos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
