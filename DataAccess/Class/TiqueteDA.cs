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

                return new ResponseGeneric<List<Tiquete>>(getTiquetes(TipoObtener.General));
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

                return new ResponseGeneric<List<Tiquete>>(getTiquetes(TipoObtener.General));
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

                return new ResponseGeneric<List<Tiquete>>(getTiquetes(TipoObtener.General));
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

                return new ResponseGeneric<List<Tiquete>>(getTiquetes(TipoObtener.Filtrados));
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
                return new ResponseGeneric<List<Tiquete>>(getTiquetes(TipoObtener.General));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Tiquete> getTiquetes(TipoObtener type)
        {
            List<Tiquete> tiquetes = new List<Tiquete>();

            List<Tiquete> tiquetesOriginales = new List<Tiquete>();

            if(type == TipoObtener.General)
            {
                tiquetesOriginales = GlobalVariables.Tiquetes;
            }
            else
            {
                tiquetesOriginales = GlobalVariables.TiquetesFiltrados;
            }

            foreach (Tiquete tiquete in tiquetesOriginales)
            {
                tiquetes.Add(new Tiquete() { 
                    idTiquete = tiquete.idTiquete,
                    idParqueo = tiquete.idParqueo,
                    nombreParqueo = GlobalVariables.Parqueos.Find(parqueo => parqueo.idParqueo == tiquete.idParqueo).Nombre,
                    idEmpleado = tiquete.idEmpleado,
                    nombreEmpleado = GlobalVariables.Empleados.Find(empleado => empleado.IdEmpleado == tiquete.idEmpleado).PrimerNombre + " " + GlobalVariables.Empleados.Find(empleado => empleado.IdEmpleado == tiquete.idEmpleado).PrimerApellido,
                    fechaIngreso = tiquete.fechaIngreso,
                    fechaSalida = tiquete.fechaSalida,
                    montoPagar = tiquete.montoPagar,
                    placa = tiquete.placa,
                    tiempoConsumido = tiquete.tiempoConsumido
                });
            }

            return tiquetes;
        }

        public enum TipoObtener
        {
            General,
            Filtrados
        }
    }
}
