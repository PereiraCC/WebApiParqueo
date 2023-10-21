using Models.Empleados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Parqueos
{
    public class Parqueo : IParqueo
    {
        public int idParqueo { get; set; }
        public string Nombre { get; set; }
        public int CantidadMaximaVehiculos { get; set; }
        public DateTime HoraApertura { get; set; }
        public DateTime HoraCierre { get; set; }
        public float TarifaHora { get; set; }
        public float TarifaMediaHora { get; set; }
    }
}
