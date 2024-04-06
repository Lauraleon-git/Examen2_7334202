using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{
    public class Detalle
    {
        [Key]
        public int IdDellate { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public string Cantidad { get; set; }
        public int Precio { get; set; }
        public string Subtotal { get; set; }

        [ForeignKey("IdPedido")]
        public virtual Pedido? Pedido { get; set; } = null!;
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; } = null!;
    }
}
