using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Estadistica.Interfaces
{
    public interface IVenta
    {
		int idVenta { get; set; }

		string NombreParqueo { get; set; }

		string NombreEmpleado { get; set; }

		DateTime fechaIngreso { get; set; }

		DateTime fechaSalida { get; set; }

		string placa { get; set; }

		float montoPagar { get; set; }

		string tiempoConsumido { get; set; }
	}
}
