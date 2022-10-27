using Dapper;
using DapperBD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Response;
using System;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Api.ReserveRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerSaludoController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DapperContext _bd;

        public VerSaludoController(IConfiguration config, DapperContext bd)
        {
            _config = config;
            _bd = bd;
        }

        [HttpGet("SaludoIntegrado")]
        public async Task<IActionResult> IntegrarSaludo(string saludoo)
        {
            var respInt = new ResponseIntegracion();
            var query = "INSERT INTO Saludos (SaludoGuardado) values (@saludo)";
            using (var client = new HttpClient())
            {
                try
                {
                    var url = _config["ApiIntegrar:Uri"];

                    var response = await client.GetAsync($"{url}?salu={saludoo}");

                    var respuesta = await response.Content.ReadAsStringAsync();

                    // bd                    
                    var connection = _bd.CreateConnection();

                    var insertSaludo = await connection.ExecuteAsync(query, new
                    {
                        saludo = saludoo
                    });

                    if (insertSaludo != 0)
                    {
                        respInt.resultado.Ok = true;
                        respInt.resultado.Message = "Saludo insertado con exito";
                    }
                    else
                    {
                        respInt.resultado.Ok = false;
                        respInt.resultado.Message = "No se pudo guardar el saludo";
                    }
             

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(respuesta);
                    }
                    return BadRequest(respuesta);

                }
                catch (Exception ex)
                {
                    respInt.resultado.Ok = false;
                    respInt.resultado.Message = ex.Message;

                }

                return Ok(respInt);
    

            }
        }
    }
}
