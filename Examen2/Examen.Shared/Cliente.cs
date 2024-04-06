using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Shared
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 20)]
        public string Apellido { get; set; }
        public string Estado { get; set; }

    }
}
