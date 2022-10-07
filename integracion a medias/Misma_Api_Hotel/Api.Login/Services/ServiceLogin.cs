using Dapper;
using DapperBD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Request;
using Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumerables.EnumerablesDB;
using static Common.HashMethods.Hashes;

namespace Api.Login.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _config;        

        public ServiceLogin(DapperContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ResponseIntegracion> IntegracionAsync(IFormFile foto, string url, string apiKey)
        {
            var response = new ResponseIntegracion();

            var guardarFotos = await GuardarDatos(foto, url, apiKey);
            

            try
            {
                var query = "INSERT INTO IntegracionApi (display_url) VALUES (@url)";

                using (var connection = _context.CreateConnection())
                {
                    var insertandoaFoto = await connection.ExecuteAsync(query, new
                    {
                        url = foto,
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }


        ///<summary>
        /// Metodo para el mensaje de bienvenida
        /// </summary>
        ///<param name="texto">text requerido para el cuerpo del mensaje</param>
        ///<returns></returns>        
        public async Task<ResponseLogin> LoginAsync(RequestLogin requestLogin)
        {
            //var response = new ResponseLogin();
            ResponseLogin response = new ResponseLogin();       
            //ResponseResultado resprueba = new ResponseResultado();

            try
            {
                var query = "SELECT * FROM Usuarios WHERE Email = @email AND Contraseña = @contraseña";
                using (var connection = _context.CreateConnection())
                {
                    var traerUsuario = (await connection.QueryAsync<Usuarios>(query, new
                    {
                        email = requestLogin.Email,
                        contraseña = HashearContraseña(requestLogin.Contraseña)
                    })).FirstOrDefault();

                    if (traerUsuario.Contraseña != null)
                    {
                        //resprueba.Token = GenerateToken(traerUsuario);
                        //response.Ok = true;
                        //response.Message = "Usuario encontrado";
                        response.Resultado.datos.Token = GenerateToken(traerUsuario);
                        response.Resultado.datos.Ok = true;
                        response.Resultado.datos.Message = "Usuario encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Resultado.datos.Ok = false;
                response.Resultado.datos.Message = ex.Message;
                //response.datos.Ok = false;
                //response.datos.Message = ex.Message;
                //_logger.LogError(ex, $"ServicioLogin.LoginAsync: Ocurrio un error. El error fue el siguiente: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseValidarUsuario> ValidarUsuarioAsync(ValidarUsuarioDto validarUsuario)
        {
            var responseValidar = new ResponseValidarUsuario();

            try
            {
                //var queryUsuarios = "SELECT * FROM Usuarios WHERE Email = @email AND Borrado NOT IN (@borrado)";
                var queryUsuarios = "select Email= @email, Borrado=@borrado from Usuarios";
                // esta segunda query es otra manera de codificar. La diferencia con la primera es q la segunda solo trae los campos q le 
                // pido, la de arriba me trae todo ( * from ) lo q tiene la tabla (Usuarios)
                using (var connection = _context.CreateConnection())
                {
                    var validandoUsuario = (await connection.QueryAsync<Usuarios>(queryUsuarios, new
                    {
                        email = validarUsuario.Email_Confirmar,
                        //borrado = EstadoLogico.Borrado
                        borrado = EstadoLogico.Activo
                    })).FirstOrDefault();
                    //})).Any();  Con el .Any validandoUsuario pasa a ser de tipo bool y solamente me dice si lo q le pido existe o no en la bd

                    if (validandoUsuario != null)
                    {
                        responseValidar.Ok = true;
                        //responseValidar.resultado.Ok = true;
                    }
                    else
                    {
                        responseValidar.Ok = false;
                        //responseValidar.resultado.Ok = false;
                        return responseValidar;
                    }
                }
            }
            catch (Exception ex)
            {
                responseValidar.Ok = false;
                responseValidar.Message = ex.Message;
                //responseValidar.resultado.Ok = false;
                //responseValidar.resultado.Message = ex.Message;
                //_logger.LogError(ex, $"ServicioLogin.LoginAsync: Ocurrio un error. El error fue el siguiente: {ex.Message}");
            }
            return responseValidar;
        }

        private string GenerateToken(Usuarios user)
        {
            var secretKey = _config.GetValue<string>("Jwt:Key");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Rol));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                // Nuestro token va a durar 2 horas
                Expires = DateTime.UtcNow.AddHours(2),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials((key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(createdToken);
        }

        // con este metodo voy al hosting y guardo la foto con los datos q necesito
        private async Task<ResponseApiExterna> GuardarDatos(IFormFile foto, string url, string apiKey)
        {
            var response = new ResponseApiExterna();

            using (var httpClient = new HttpClient())
            {
                // el verbo post necesita enviar el contenido ademas de httpclient(url)
                var ms = new MemoryStream();
                await foto.CopyToAsync(ms);

                var imageContent = new ByteArrayContent(ms.ToArray());

                var content = new MultipartFormDataContent();
                content.Add(imageContent, "image", "image.jpg");

                var task = await httpClient.PostAsync($"{url}?key={apiKey}", content);

                var taskResponse = await task.Content.ReadAsStringAsync();
                //taskResponse.Wait();
                response = JsonConvert.DeserializeObject<ResponseApiExterna>(taskResponse);
            }

            return response;
        }

    }
}
