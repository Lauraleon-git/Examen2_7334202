using Examen.Api.Contratos;
using Examen.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Api.Implementacion
{
    public class ProductoLogic : IProductoLogic
    {
        private readonly Contexto contexto;
        public ProductoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> Insertar(Producto producto)
        {
            bool sw = false;
            contexto.Productos.Add(producto);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Producto>> ListarProductosTodos()
        {
            var list = await contexto.Productos.ToListAsync();
            return list;
        }

        public async Task<List<Producto>> ListarRangoFechas(DateTime f1, DateTime f2)
        {
            var lista=await contexto.Detalles.Where(x=>x.Pedido.Fecha>=f1 && x.Pedido.Fecha<=f2).OrderByDescending(a=>a.Cantidad)
                .Select(p=>new Producto
                {
                    IdProducto=p.Producto.IdProducto,
                    NombreProducto=p.Producto.NombreProducto
                }).ToListAsync();
            return lista;
        }
    }
}
