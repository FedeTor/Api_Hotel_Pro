using Microsoft.AspNetCore.Http;
using Models;
using Models.Request;
using Models.Response;
using System.Threading.Tasks;

namespace Api.Login.Services
{
    public interface IServiceLogin
    {
        Task<ResponseLogin> LoginAsync(RequestLogin requestLogin);

        Task<ResponseIntegracion> IntegracionAsync(IFormFile foto, string url, string apiKey);
        Task<ResponseValidarUsuario> ValidarUsuarioAsync(ValidarUsuarioDto validarUsuario);


    }
}
