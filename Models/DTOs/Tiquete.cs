using System;
using System.Collections.Generic;

namespace Models.DTOs;

public partial class Tiquete
{
    public int Idtiquete { get; set; }

    public int Idparqueo { get; set; }

    public string? NombreParqueo { get; set; }

    public int Idempleado { get; set; }

    public string? NombreEmpleado { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaSalida { get; set; }

    public string? Placa { get; set; }

    public string? MontoPagar { get; set; }

    public string? TiempoConsumido { get; set; }

    public bool? Venta { get; set; }

    public virtual Empleado IdempleadoNavigation { get; set; } = null!;

    public virtual Parqueo IdparqueoNavigation { get; set; } = null!;
}
