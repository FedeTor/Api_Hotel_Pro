using DapperBD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Request;
using Models.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrarApi : Controller
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _config;

        public IntegrarApi(DapperContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> IntegracionApi(RequestLogin requestLogin)
        {
            var response = new ResponseIntegracion();

            var url = "https://localhost:10/api/Login";

            using (var client = new HttpClient())
            {
                var resultado = await client.GetAsync(url);
            }


            //var httpC = new HttpClient();
            //var json = await httpC.GetStringAsync("https://localhost:10/api/Login"); // lo llamo json xq devuelve un formato json

            //if (json != null)
            //{
            //    response.resultado.Ok = true;
            //    response.resultado.Message = "Datos del usuario logueado";
            //    return BadRequest();
            //}
            return Ok(response);
        }
    }
}
