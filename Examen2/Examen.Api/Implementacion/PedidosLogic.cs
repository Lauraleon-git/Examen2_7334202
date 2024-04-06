using Examen.Api.Contratos;
using Examen.Api.Reportes;
using Examen.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
        public async Task<bool> EliminarPedido(int id)
        {
            bool sw = false;
            Pedido existe = await contexto.Pedidos.FindAsync(id);
            if (existe != null)
            {
                contexto.Pedidos.Remove(existe);
                await contexto.SaveChangesAsync();
                sw = true;
            }
            return sw;
        }

        public async Task<bool> InsertarPedido(Pedido pedido)
        {
            bool sw = false;
           
            contexto.Pedidos.Add(pedido);
            contexto.SaveChanges();
            foreach(var i in pedido.Detalles)
            {
                
                contexto.Detalles.Add(i);


            }

            int response = await contexto.SaveChangesAsync();
            if (response >= 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Pedido>> ListarPedidosTodos()
        {
            var lista = await contexto.Pedidos.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarPedido(Pedido pedido, int id)
        {
            bool sw = false;
            Pedido edit = await contexto.Pedidos.FindAsync();
            if (edit != null)
            {
                edit.IdCliente = pedido.IdCliente;
                edit.Fecha = pedido.Fecha;
                edit.Total = pedido.Total;
                edit.Estado = pedido.Estado;
                await contexto.SaveChangesAsync();
                sw = true;
            }
            return sw;
        }

        public async Task<Pedido> ObtenerPedidosById(int id)
        {
            Pedido pedido = await contexto.Pedidos.FirstOrDefaultAsync(x => x.IdPedido == id);
            return pedido;
        }

        public async Task<List<Reporte1>> Reporte1()
        {
            var lista = await contexto.Detalles.Include(p=>p.Producto).Include(ped=>ped.Pedido).Select(x=> new Reporte1
            {
                nombre=x.Pedido.Cliente.Nombre,
                FechaPedido=x.Pedido.Fecha,
                NombreProducto=x.Producto.NombreProducto,
                Subtotal=x.Subtotal

            }).ToListAsync();   
            return lista;
            
        }
        public async Task<List<ProductoMasPedido>>MasPedidodeProductos()
        {
            var lista = await contexto.Detalles.Include(p=>p.Producto).GroupBy(dp => dp.IdProducto)
                .Select(x => new ProductoMasPedido
                {
                    nombre=x.First().Producto.NombreProducto,
                    Cantidad=x.Count()

                }).OrderByDescending(c=>c.Cantidad).Take(3).ToListAsync();
            return lista;
        }

       
    }

   
   
}
