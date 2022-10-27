using Dapper;
using DapperBD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Request;
using Models.Response;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public Task<ResponseIntegracion> IntegrarApi(RequestLogin requestLogin)
        {
            throw new NotImplementedException();
        }


        ///<summary>
        /// Metodo para el mensaje de bienvenida
        /// </summary>
        ///<param name="texto">text requerido para el cuerpo del mensaje</param>
        ///<returns></returns>        
        public async Task<ResponseLogin> LoginAsync(RequestLogin requestLogin)
        {
            var response = new ResponseLogin();

            try
            {
                var query = "SELECT * FROM Usuarios WHERE Email = @email AND Contraseña = @contraseña";                
                using( var connection = _context.CreateConnection())
                {
                    var traerUsuario = (await connection.QueryAsync<Usuarios>(query, new
                    {
                        @email = requestLogin.Email,
                        @contraseña = HashearContraseña(requestLogin.Contraseña)
                    })).FirstOrDefault();

                    if (traerUsuario.Contraseña != null)
                    {
                        response.Token = GenerateToken(traerUsuario);
                        response.Ok = true;
                        response.Message = "Usuario encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                //_logger.LogError(ex, $"ServicioLogin.LoginAsync: Ocurrio un error. El error fue el siguiente: {ex.Message}");
            }
            return response;
        }

        public async Task<ResponseValidarUsuario> ValidarUsuarioAsync(ValidarUsuarioDto validarUsuario)
        {
            var responseValidar = new ResponseValidarUsuario();

            try
            {
                var queryUsuarios = "SELECT * FROM Usuarios WHERE Email = @email AND Borrado NOT IN (@borrado)";
                using(var connection = _context.CreateConnection())
                {
                    var validandoUsuario = (await connection.QueryAsync<Usuarios>(queryUsuarios, new
                    {
                        email = validarUsuario.Email_Confirmar,
                        borrado = EstadoLogico.Borrado
                    })).FirstOrDefault();

                    if(validandoUsuario != null)
                    {
                        responseValidar.Ok = true;                        
                    }
                    else
                    {
                        responseValidar.Ok = false;
                        return responseValidar;
                    }                    
                }                
            }
            catch (Exception ex)
            {
                responseValidar.Ok = false;
                responseValidar.Message = ex.Message;
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
    }
}
