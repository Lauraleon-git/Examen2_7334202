using Examen.Api.Contratos;
using Examen.Api.Implementacion;
using Examen.Api.Reportes;
using Examen.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Examen.Api.EndPoints
{
    public class PedidoFunction
    {
        private readonly ILogger<PedidoFunction> _logger;
        private readonly IPedidoLogic repos;


        public PedidoFunction(ILogger<PedidoFunction> logger, IPedidoLogic repos)
        {
            _logger = logger;
            this.repos = repos;

        }
        [Function("ListarPedidos")]
        [OpenApiOperation("Listarspec", "ListarPedidos", Description = "Sirve para listar todas los Pedidos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Pedido>),
         Description = "Mostrara una lista de Idiomas")]

        public async Task<HttpResponseData> ListarIdiomas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarIdiomas")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Listar Idiomas");
            try
            {
                var lista = repos.ListarPedidosTodos();
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
        [Function("InsertarPedido")]
        [OpenApiOperation("Insertarspec", "InsertarIdioma", Description = "Sirve para Insertar una Idioma")]
        [OpenApiRequestBody("application/json", typeof(Pedido), Description = "pedido modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Pedido), Description = "Mostrara el Pedido Creado")]

        public async Task<HttpResponseData> InsertarPedido([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarpedido")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Insertar Persona");
            try
            {
                var idi = await req.ReadFromJsonAsync<Pedido>() ?? throw new Exception("Debe ingresar un pedido con todos sus datos");
                bool seGuardo = await repos.InsertarPedido(idi);
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
        [Function("ListaReporte1")]
        [OpenApiOperation("Listarspec", "ListaReporte1", Description = "Sirve para listar todas los Pedidos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Reporte1>),
        Description = "Mostrara una lista de Idiomas")]

        public async Task<HttpResponseData> ListaReporte1([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listaReporte1s")] HttpRequestData req)
        {
            _logger.LogInformation("Ejecutando Azure Function para Listar Idiomas");
            try
            {
                var listaIdioma = repos.Reporte1();
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(listaIdioma.Result);
                return respuesta;
            }
            catch (Exception e)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(e.Message);
                return error;
            }

        }

            [Function("ListaReporte2")]
            [OpenApiOperation("Listarspec", "ListaReporte1", Description = "Sirve para listar reporte2")]
            [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ProductoMasPedido>),
        Description = "Mostrara una lista de reportes")]

            public async Task<HttpResponseData> ListaReporte2([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listaReporte2")] HttpRequestData req)
            {
                _logger.LogInformation("Ejecutando Azure Function para listar el reporte2");
                try
                {
                    var listaIdioma = repos.MasPedidodeProductos();
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    await respuesta.WriteAsJsonAsync(listaIdioma.Result);
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
       



        
