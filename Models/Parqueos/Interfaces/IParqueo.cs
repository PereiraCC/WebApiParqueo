using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados.Interfaces
{
    public interface IParqueo
    {
        int idParqueo { get; set; }

        string Nombre { get; set; }

        int CantidadMaximaVehiculos { get; set; }

        DateTime HoraApertura { get; set; }

        DateTime HoraCierre { get; set; }

        float TarifaHora { get; set; }

        float TarifaMediaHora { get; set; }
    }
}
