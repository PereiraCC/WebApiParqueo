﻿using Models.Empleados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class Empleados : IEmpleados
    {
        public int IdEmpleado { get; set; }
        public int idParqueo { get; set; }
        public string NumeroEmpleado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string PersonaContacto { get; set; }
    }
}
