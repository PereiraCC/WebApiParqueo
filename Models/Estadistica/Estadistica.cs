using Models.Estadistica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Estadistica
{
    public class Estadistica : IEstadistica
    {
        public int idEstadistica { get; set; }
        public float montoGenerado { get; set; }
        public List<Venta> ventas { get; set; }
    }
}
