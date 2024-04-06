using Examen.Api.Contratos;
using Examen.Api.Implementacion;
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

        public PedidoFunction(ILogger<PedidoFunction> logger,IPedidoLogic repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarPedido")]
        [OpenApiOperation("Insertarspec", "InsertarIdioma", Description = "Sirve para Insertar una Idioma")]
        [OpenApiRequestBody("application/json", typeof(Pedido), Description = "Idioma modelo")]
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

       
    }
}
