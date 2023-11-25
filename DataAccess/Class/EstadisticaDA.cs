using System;
using Models.Estadistica;
using Models.General;
using Models.Parqueos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Models.DTOs;
using Microsoft.Extensions.Options;

namespace DataAccess.Class
{
    public class EstadisticaDA : IEstadisticaDA
    {
        private ProyectoParqueoContext _parqueoEntity;

        public EstadisticaDA(IOptions<ConfiguracionParqueo> options)
        {
            _parqueoEntity = new ProyectoParqueoContext(options.Value.ConnectionParqueo);
        }

        //public ResponseGeneric<Estadistica> addValue(Venta venta)
        //{
        //    try
        //    {
        //        // Se calcula el monto
        //        GlobalVariables.Estadistica.montoGenerado += venta.montoPagar;
        //        GlobalVariables.EstadisticaFiltrados.montoGenerado += venta.montoPagar;

        //        if (GlobalVariables.Estadistica.ventas.Count() > 0)
        //        {
        //            int ultimoId = GlobalVariables.Estadistica.ventas.LastOrDefault().idVenta;
        //            venta.idVenta = ultimoId + 1;
        //        }
        //        else
        //        {
        //            venta.idVenta = 1;
        //        }

        //        // Se agrega el nuevo tiquete
        //        GlobalVariables.Estadistica.ventas.Add(venta);

        //        return new ResponseGeneric<Estadistica>(GlobalVariables.Estadistica);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ResponseGeneric<Estadistica> searchValue(string valor, Models.Enums.EnumSearchEstadistica tipo)
        {
            try
            {
                List<Models.DTOs.Tiquete> tiquetes = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Venta == true).ToList();
                List<Models.DTOs.Tiquete> searchTiquetes = new List<Models.DTOs.Tiquete>();

                switch (tipo)
                {
                    case Models.Enums.EnumSearchEstadistica.Mes:
                        searchTiquetes = tiquetes.Where( estadistica =>
                            estadistica.FechaIngreso?.Month == Int32.Parse(valor) || estadistica.FechaSalida?.Month == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.Dia:
                        searchTiquetes = tiquetes.Where(estadistica =>
                            estadistica.FechaIngreso?.Day == Int32.Parse(valor) || estadistica.FechaSalida?.Day == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.Tiempo:
                        searchTiquetes = tiquetes.Where(estadistica =>
                            estadistica.FechaIngreso?.Hour == Int32.Parse(valor) || estadistica.FechaSalida?.Hour == Int32.Parse(valor)).ToList();
                        break;

                    case Models.Enums.EnumSearchEstadistica.ParqueosVendeMas:

                        // Parqueos
                        List<Models.DTOs.Parqueo> parqueos = _parqueoEntity.Parqueos.ToList();

                        //Ventas
                        List<Models.DTOs.Tiquete> ventas = _parqueoEntity.Tiquetes.Where(tiquete => tiquete.Venta == true).ToList();

                        // Los resultados
                        List <VentasParqueos> resultados = new List<VentasParqueos>(parqueos.Count);

                        // Se obtiene los parqueos actuales
                        List<string> nameParqueos = new List<string>(parqueos.Count);

                        foreach(Models.DTOs.Parqueo parqueo in parqueos)
                        {
                            nameParqueos.Add(parqueo.Nombre);
                        }

                        foreach(string nameParqueo in nameParqueos)
                        {
                            float monto = 0;

                            List<Models.DTOs.Tiquete> searchVentas = ventas.Where(estadistica =>
                                estadistica.NombreParqueo.Equals(nameParqueo)).ToList();

                            foreach (Models.DTOs.Tiquete venta in ventas)
                            {
                                monto += (venta.MontoPagar != null) ? float.Parse(venta.MontoPagar) : 0;
                            }

                            resultados.Add(new VentasParqueos() { 
                                montoGenerado = monto,
                                nombreParqueo = nameParqueo
                            });
                        }

                        string bestParqueo = resultados.OrderByDescending(result => result.montoGenerado).First().nombreParqueo;

                        searchTiquetes = ventas.Where(estadistica =>
                            estadistica.NombreParqueo.Equals(bestParqueo)).ToList();

                        break;
                }

                return new ResponseGeneric<Estadistica>(formatEstadistica(searchTiquetes));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseGeneric<Models.Estadistica.Estadistica> getAll()
        {
            try
            {
                List<Models.DTOs.Tiquete> tiquetes = _parqueoEntity.Tiquetes.Where( tiquete => tiquete.Venta == true ).ToList();
                return new ResponseGeneric<Models.Estadistica.Estadistica>(formatEstadistica(tiquetes));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Models.Estadistica.Estadistica formatEstadistica(List<Models.DTOs.Tiquete> tiquetes)
        {
            Models.Estadistica.Estadistica estadistica = new Models.Estadistica.Estadistica();
            List<Models.Estadistica.Venta> ventas = new List<Models.Estadistica.Venta>();

            int indexVenta = 1;
            float montoGenerado = 0;

            foreach (Models.DTOs.Tiquete tiquete in tiquetes)
            {
                Models.DTOs.Parqueo seletedParqueo = _parqueoEntity.Parqueos.Find(tiquete.Idparqueo);
                Models.DTOs.Empleado seletedEmpleado = _parqueoEntity.Empleados.Find(tiquete.Idempleado);

                montoGenerado += (tiquete.MontoPagar != null) ? float.Parse(tiquete.MontoPagar) : 0;

                ventas.Add(new Models.Estadistica.Venta()
                {
                    idVenta = indexVenta++,
                    NombreParqueo = (seletedParqueo != null) ? seletedParqueo.Nombre : "",
                    NombreEmpleado = (seletedEmpleado == null) ? "" : seletedEmpleado.PrimerNombre + " " + seletedEmpleado.PrimerApellido,
                    fechaIngreso = tiquete.FechaIngreso ?? DateTime.Now,
                    fechaSalida = tiquete.FechaSalida ?? DateTime.Now,
                    montoPagar = (tiquete.MontoPagar != null) ? float.Parse(tiquete.MontoPagar) : 0,
                    placa = tiquete.Placa,
                    tiempoConsumido = tiquete.TiempoConsumido
                });
            }

            estadistica.idEstadistica = 1;
            estadistica.montoGenerado = montoGenerado;
            estadistica.ventas = ventas;

            return estadistica;
        }
    }

    internal class VentasParqueos
    {
        public string nombreParqueo { get; set; }

        public float montoGenerado { get; set; }
    }
}
