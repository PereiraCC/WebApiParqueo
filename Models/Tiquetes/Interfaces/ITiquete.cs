﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tiquetes.Interfaces
{
    public interface ITiquete
    {
        public int idTiquete { get; set; }

        public int idParqueo { get; set; }

        public int idEmpleado { get; set; }

        public DateTime fechaIngreso { get; set; }

        public DateTime? fechaSalida { get; set; }

        public string placa { get; set; }

        public float? montoPagar { get; set; }

        public string? tiempoConsumido { get; set; }
    }
}
