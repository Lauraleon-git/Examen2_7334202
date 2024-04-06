using Examen.Api.Implementacion;
using Examen.Api.Reportes;
using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Api.Contratos
{
    public interface IPedidoLogic
    {
        public Task<bool> InsertarPedido(Pedido pedido);
        public Task<bool> ModificarPedido(Pedido pedido, int id);
        public Task<bool> EliminarPedido(int id);
        public Task<List<Pedido>> ListarPedidosTodos();
        public Task<Pedido> ObtenerPedidosById(int id);
        public  Task<List<Reporte1>>Reporte1();
        public Task<List<ProductoMasPedido>> MasPedidodeProductos();
    }
}
