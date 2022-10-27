using Api.Login.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Request;
using Models.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IServiceLogin _login;
        //private readonly IConfiguration _config;

        public LoginController(IServiceLogin login)
        {
            _login = login;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm] RequestLogin requestLogin)
        {
            var responseValidacion = new ResponseValidarUsuario();
            var responseLogin = new ResponseLogin();

            // valido q los campos estén completos
            if (requestLogin.Email == null || requestLogin.Contraseña == null)
            {
                responseValidacion.Ok = false;
                responseValidacion.Message = "Los campos no puede estar vacios";
                return BadRequest(responseValidacion);
            }
            else
            {
                var validacionUsser = new ValidarUsuarioDto
                {
                    Email_Confirmar = requestLogin.Email
                };

                //valido q el usuario exista
                var verificandoUsuario = await _login.ValidarUsuarioAsync(validacionUsser);
                if (verificandoUsuario.Ok)
                {
                    var buscarUsuario = await _login.LoginAsync(requestLogin);
                    if (buscarUsuario.Ok)
                    {
                        responseLogin = buscarUsuario;
                        return Ok(responseLogin);
                    }
                    else
                    {
                        //valido q los datos sean exactos
                        responseLogin.Ok = false;
                        responseLogin.Message = "Los datos no coinciden";
                        return BadRequest(responseLogin);
                    }
                }
                else
                {
                    responseLogin.Ok = false;
                    responseLogin.Message = "El Usuario no existe";
                    return BadRequest(responseValidacion);
                }
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> IntegrarApi()
        //{
        //    var response = new ResponseIntegracion();
        //    //var url = _config["AppSettings:ApiIntegracion:Uri"];

        //    var cliente = new HttpClient();
        //    var json = await cliente.GetStringAsync("https://localhost:10/api/Login"); // lo llamo json xq devuelve un formato json

        //    if (json != null)
        //    {
        //        response.resultado.Ok = true;
        //        response.resultado.Message = "Datos del usuario logueado";
        //        return BadRequest();
        //    }
        //    return Ok(response);
        //}

    }
}
