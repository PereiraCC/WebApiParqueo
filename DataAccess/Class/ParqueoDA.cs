using System;
using Models.Empleados;
using Models.Parqueos;
using Models.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Class
{
    public class ParqueoDA : IParqueoDA
    {
        public ParqueoDA()
        {
        }

        public ResponseGeneric<List<Parqueo>> addValue(Parqueo parqueo)
        {
            try
            {
                // Se agrega el nuevo empleado
                GlobalVariables.Parqueos.Add(parqueo);

                return new ResponseGeneric<List<Parqueo>>(GlobalVariables.Parqueos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Parqueo>> deleteValue(int idParqueo)
        {
            try
            {
                // Se busca el index de tiquete a eliminar
                int indexParqueo = GlobalVariables.Parqueos.FindIndex(parqu => parqu.idParqueo == idParqueo);

                if (indexParqueo == -1)
                {
                    return new ResponseGeneric<List<Parqueo>>("No se encontro el parqueo");
                }

                // Se eliminar el tiquete
                Models.General.GlobalVariables.Parqueos.RemoveAt(indexParqueo);

                return new ResponseGeneric<List<Parqueo>>(GlobalVariables.Parqueos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Parqueo>> editValue(Parqueo parqueo, int idParqueo)
        {
            try
            {
                // Se busca el index de tiquete a modificar
                int indexParqueo = GlobalVariables.Parqueos.FindIndex(parqu => parqu.idParqueo == idParqueo);

                // Se valida que index sea correcto
                if (indexParqueo != -1)
                {
                    // Se modifica el objeto
                    GlobalVariables.Parqueos[indexParqueo].Nombre = parqueo.Nombre;
                    GlobalVariables.Parqueos[indexParqueo].CantidadMaximaVehiculos = parqueo.CantidadMaximaVehiculos;
                    GlobalVariables.Parqueos[indexParqueo].HoraApertura = parqueo.HoraApertura;
                    GlobalVariables.Parqueos[indexParqueo].HoraCierre = parqueo.HoraCierre;
                    GlobalVariables.Parqueos[indexParqueo].TarifaHora = parqueo.TarifaHora;
                    GlobalVariables.Parqueos[indexParqueo].TarifaMediaHora = parqueo.TarifaMediaHora;
                }

                return new ResponseGeneric<List<Parqueo>>(GlobalVariables.Parqueos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Parqueo>> searchValue(string valor, Models.Enums.EnumSearchParqueo tipo)
        {
            try
            {
                switch (tipo)
                {
                    case Models.Enums.EnumSearchParqueo.Nombre:
                        GlobalVariables.ParqueosFiltrados = GlobalVariables.Parqueos.Where(parqueo => parqueo.Nombre.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchParqueo.CantididadVehiculos:
                        GlobalVariables.ParqueosFiltrados = GlobalVariables.Parqueos.Where(parqueo => parqueo.CantidadMaximaVehiculos == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchParqueo.Tarifa:
                        GlobalVariables.ParqueosFiltrados = GlobalVariables.Parqueos.Where(parqueo => parqueo.TarifaHora == Int32.Parse(valor) || parqueo.TarifaMediaHora == Int32.Parse(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Parqueo>>(GlobalVariables.ParqueosFiltrados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
