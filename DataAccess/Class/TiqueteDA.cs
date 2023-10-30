using System;
using Models.Tiquetes;
using Models.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Class
{
    public class TiqueteDA : ITiqueteDA
    {
        public TiqueteDA()
        {
        }

        public ResponseGeneric<List<Tiquete>> addValue(Tiquete tiquete)
        {
            try
            {
                // Se agrega el nuevo empleado
                GlobalVariables.Tiquetes.Add(tiquete);

                return new ResponseGeneric<List<Tiquete>>(GlobalVariables.Tiquetes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Tiquete>> deleteValue(int idTiquete)
        {
            try
            {
                // Se busca el index de tiquete a eliminar
                int indexTiquete = GlobalVariables.Tiquetes.FindIndex(tique => tique.idTiquete == idTiquete);

                if (indexTiquete == -1)
                {
                    return new ResponseGeneric<List<Tiquete>>("No se encontro el tiquete");
                }

                // Se eliminar el tiquete
                Models.General.GlobalVariables.Tiquetes.RemoveAt(indexTiquete);

                return new ResponseGeneric<List<Tiquete>>(GlobalVariables.Tiquetes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Tiquete>> editValue(Tiquete tiquete, int idTiquete)
        {
            try
            {
                // Se busca el index de tiquete a modificar
                int indexTiquete = GlobalVariables.Tiquetes.FindIndex(tique => tique.idTiquete == idTiquete);

                // Se valida que index sea correcto
                if (indexTiquete != -1)
                {
                    // Se modifica el objeto
                    GlobalVariables.Tiquetes[indexTiquete].fechaSalida = tiquete.fechaSalida;
                    GlobalVariables.Tiquetes[indexTiquete].montoPagar = tiquete.montoPagar;
                    GlobalVariables.Tiquetes[indexTiquete].tiempoConsumido = tiquete.tiempoConsumido;
                }

                return new ResponseGeneric<List<Tiquete>>(GlobalVariables.Tiquetes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Tiquete>> searchValue(string valor, Models.Enums.EnumSearchTiquete tipo)
        {
            try
            {
                switch (tipo)
                {
                    case Models.Enums.EnumSearchTiquete.Placa:
                        GlobalVariables.TiquetesFiltrados = GlobalVariables.Tiquetes.Where(tiquete => tiquete.placa.Contains(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Numero:
                        GlobalVariables.TiquetesFiltrados = GlobalVariables.Tiquetes.Where(tiquete => tiquete.idTiquete == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Parqueo:
                        GlobalVariables.TiquetesFiltrados = GlobalVariables.Tiquetes.Where(tiquete => tiquete.idParqueo == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchTiquete.Empleado:
                        GlobalVariables.TiquetesFiltrados = GlobalVariables.Tiquetes.Where(tiquete => tiquete.idEmpleado == Int32.Parse(valor)).ToList();
                        break;
                }

                return new ResponseGeneric<List<Tiquete>>(GlobalVariables.TiquetesFiltrados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<List<Tiquete>> getAll()
        {
            try
            {
                return new ResponseGeneric<List<Tiquete>>(GlobalVariables.Tiquetes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
