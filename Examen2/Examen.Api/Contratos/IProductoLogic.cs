using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Api.Contratos
{
    public interface IProductoLogic
    {
        public Task<List<Producto>> ListarProductosTodos();
        public Task<bool> Insertar(Producto producto);
        public Task<List<Producto>> ListarRangoFechas(DateTime f1, DateTime f2);
    }
}
