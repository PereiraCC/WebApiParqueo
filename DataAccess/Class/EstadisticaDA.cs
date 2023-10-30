using System;
using Models.Estadistica;
using Models.General;
using Models.Parqueos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Class
{
    public class EstadisticaDA : IEstadisticaDA
    {
        public EstadisticaDA()
        {
        }

        public ResponseGeneric<Estadistica> addValue(Venta venta)
        {
            try
            {
                // Se calcula el monto
                GlobalVariables.Estadistica.montoGenerado += venta.montoPagar;
                GlobalVariables.EstadisticaFiltrados.montoGenerado += venta.montoPagar;

                if (GlobalVariables.Estadistica.ventas.Count() > 0)
                {
                    int ultimoId = GlobalVariables.Estadistica.ventas.LastOrDefault().idVenta;
                    venta.idVenta = ultimoId + 1;
                }
                else
                {
                    venta.idVenta = 1;
                }

                // Se agrega el nuevo tiquete
                GlobalVariables.Estadistica.ventas.Add(venta);

                return new ResponseGeneric<Estadistica>(GlobalVariables.Estadistica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<Estadistica> searchValue(string valor, Models.Enums.EnumSearchEstadistica tipo)
        {
            try
            {
                GlobalVariables.EstadisticaFiltrados.montoGenerado = 0;

                switch (tipo)
                {
                    case Models.Enums.EnumSearchEstadistica.Mes:
                        GlobalVariables.EstadisticaFiltrados.ventas = GlobalVariables.Estadistica.ventas.Where( estadistica =>
                            estadistica.fechaIngreso.Month == Int32.Parse(valor) || estadistica.fechaSalida.Month == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.Dia:
                        GlobalVariables.EstadisticaFiltrados.ventas = GlobalVariables.Estadistica.ventas.Where(estadistica =>
                            estadistica.fechaIngreso.Day == Int32.Parse(valor) || estadistica.fechaSalida.Day == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.Tiempo:
                        GlobalVariables.EstadisticaFiltrados.ventas = GlobalVariables.Estadistica.ventas.Where(estadistica =>
                            estadistica.fechaIngreso.ToString("HH:mm").Equals(valor) || estadistica.fechaSalida.ToString("HH:mm").Equals(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.ParqueosVendeMas:

                        // Los resultados
                        List<VentasParqueos> resultados = new List<VentasParqueos>(GlobalVariables.Parqueos.Count);

                        // Se obtiene los parqueos actuales
                        List<string> nameParqueos = new List<string>(GlobalVariables.Parqueos.Count);

                        foreach(Parqueo parqueo in GlobalVariables.Parqueos)
                        {
                            nameParqueos.Add(parqueo.Nombre);
                        }

                        foreach(string nameParqueo in nameParqueos)
                        {
                            float monto = 0;

                            List<Venta> ventas = GlobalVariables.Estadistica.ventas.Where(estadistica =>
                                estadistica.NombreParqueo.Equals(nameParqueo)).ToList();

                            foreach (Venta venta in ventas)
                            {
                                monto += venta.montoPagar;
                            }

                            resultados.Add(new VentasParqueos() { 
                                montoGenerado = monto,
                                nombreParqueo = nameParqueo
                            });
                        }

                        string bestParqueo = resultados.OrderByDescending(result => result.montoGenerado).First().nombreParqueo;

                        GlobalVariables.EstadisticaFiltrados.ventas = GlobalVariables.Estadistica.ventas.Where(estadistica =>
                            estadistica.NombreParqueo.Equals(bestParqueo)).ToList();

                        break;
                }

                foreach (Venta venta in GlobalVariables.EstadisticaFiltrados.ventas)
                {
                    GlobalVariables.EstadisticaFiltrados.montoGenerado += venta.montoPagar;
                }

                return new ResponseGeneric<Estadistica>(GlobalVariables.EstadisticaFiltrados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<Estadistica> getAll()
        {
            try
            {
                return new ResponseGeneric<Estadistica>(GlobalVariables.Estadistica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    internal class VentasParqueos
    {
        public string nombreParqueo { get; set; }

        public float montoGenerado { get; set; }
    }
}
