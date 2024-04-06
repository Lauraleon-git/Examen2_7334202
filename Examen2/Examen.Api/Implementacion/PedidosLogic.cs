using Examen.Api.Contratos;
using Examen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Examen.Api.Implementacion.PedidosLogic;

namespace Examen.Api.Implementacion
{

    public class PedidosLogic : IPedidoLogic
    {
       
            private readonly Contexto contexto;

            public PedidosLogic(Contexto contexto)
            {
                this.contexto = contexto;
            }
            public Task<bool> EliminarPedido(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertarPedido(Pedido pedido)
        {
            bool sw = false;
            contexto.Pedidos.Add(pedido);
            

            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public Task<List<Pedido>> ListarPedidosTodos()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModificarPedido(Pedido pedido, int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> ObtenerPedidosById(int id)
        {
            throw new NotImplementedException();
        }
        

    }
    


}
