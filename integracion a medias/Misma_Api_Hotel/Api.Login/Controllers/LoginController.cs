using Api.Login.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Request;
using Models.Response;
using System.Threading.Tasks;

namespace Api.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IServiceLogin _login;

        public LoginController(IServiceLogin login)
        {
            _login = login;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm] RequestLogin requestLogin)
        {
            var responseValidacion = new ResponseValidarUsuario();
            var responseLogin = new ResponseLogin();
            //var responseLogin = new ResponseResultado();

            // valido q los campos estén completos
            // Probar string.IsNullOrEmpty(requestLogin.Email)
            //if (requestLogin.Email == null || requestLogin.Contraseña == null)
            if (string.IsNullOrEmpty(requestLogin.Email) || string.IsNullOrEmpty(requestLogin.Contraseña))
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
                    if (buscarUsuario.Resultado.datos.Ok)
                    {
                        responseLogin = buscarUsuario;
                        return Ok(responseLogin);
                    }
                    else
                    {
                        //valido q los datos sean exactos
                        responseLogin.Resultado.datos.Ok = false;
                        responseLogin.Resultado.datos.Message = "Los datos no coinciden";
                        return BadRequest(responseLogin);
                    }                    
                }
                else
                {
                    responseLogin.Resultado.datos.Ok = false;
                    responseLogin.Resultado.datos.Message = "El Usuario no existe";
                    return BadRequest(responseValidacion);
                }
            }
        }

        [HttpPost("SubirFotos")]
        public async Task<IActionResult> IntegracionAsync([FromForm] IFormFile foto, [FromForm] string url, [FromForm] string apiKey)            
        {
            var response = new ResponseIntegracion();

            var agregarFoto = await _login.IntegracionAsync(foto, url, apiKey);

            response = agregarFoto;
            return Ok(response);
        }
    }
}
