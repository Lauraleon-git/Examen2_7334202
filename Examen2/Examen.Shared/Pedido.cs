using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }

        [StringLength(maximumLength: 20)]
        public string Estado { get; set; }
        public virtual List<Detalle> Detalles { get; set; } = new List<Detalle>();


        [ForeignKey("IdCliente")]
        public virtual Cliente? Cliente { get; set; } = null!;
    }
}
