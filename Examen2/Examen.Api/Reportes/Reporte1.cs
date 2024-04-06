using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Api.Reportes
{
   
        public class Reporte1
        {
            public string nombre { get; set; } = null!;
            public DateTime FechaPedido { get; set; }
            public string NombreProducto { get; set; } = null!;
            public double Subtotal { get; set; }
        }
    }

