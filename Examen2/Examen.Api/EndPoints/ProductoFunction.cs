using Examen.Api.Contratos;
using Examen.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Examen.Api.EndPoints
{
    public class ProductoFunction
    {
        private readonly ILogger<ProductoFunction> _logger;
        private readonly IProductoLogic repos;

        public ProductoFunction(ILogger<ProductoFunction> logger, IProductoLogic repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("ListarProducto")]
        [OpenApiOperation("Listarspec", "ListarProductos", Description = "Sirve para listar todas los Pedidos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Producto>),
         Description = "Mostrara una lista de Idiomas")]

        public async Task<HttpResponseData> ListarProductos([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarProducto")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Listar productos");
            try
            {
                var lista = repos.ListarProductosTodos();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }
        [Function("InsertarProducto")]
        [OpenApiOperation("Insertarspec", "InsertarProducto", Description = "Sirve para Insertar una producto")]
        [OpenApiRequestBody("application/json", typeof(Pedido), Description = "pedido modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Producto), Description = "Mostrara el producto Creado")]

        public async Task<HttpResponseData> InsertarPedido([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarProducto")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Insertar Persona");
            try
            {
                var idi = await req.ReadFromJsonAsync<Producto>() ?? throw new Exception("Debe ingresar un pedido con todos sus datos");
                bool seGuardo = await repos.Insertar(idi);
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

        [Function("ListarPorRangoFechas")]
        [OpenApiOperation("Listarspec", "ListarRangoFechas", Description = "Sirve para listar todas los Pedidos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Producto>),
         Description = "Mostrara una lista fechas")]
        [OpenApiParameter(name: "f1", In = ParameterLocation.Path, Required = true, Type = typeof(DateTime))]
        [OpenApiParameter(name: "f2", In = ParameterLocation.Path, Required = true, Type = typeof(DateTime))]



        public async Task<HttpResponseData> ListarRangoFechas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarfechas/{f1}/{f2}")] HttpRequestData req,DateTime f1,DateTime f2)
        {
            _logger.LogInformation("Ejecutando Azure Function para Listar productos");
            try
            {
                var lista = repos.ListarRangoFechas(f1,f2);
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }
        }
    }
}
