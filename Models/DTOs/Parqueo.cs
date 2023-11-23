using System;
using System.Collections.Generic;

namespace Models.DTOs;

public partial class Parqueo
{
    public int Idparqueo { get; set; }

    public string? Nombre { get; set; }

    public int? CantidadMaximaVehiculos { get; set; }

    public DateTime? HoraApertura { get; set; }

    public DateTime? HoraCierre { get; set; }

    public double? TarifaHora { get; set; }

    public double? TarifaMediaHora { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Tiquete> Tiquetes { get; set; } = new List<Tiquete>();
}
