using Models;
using Models.Request;
using Models.Response;
using System.Threading.Tasks;

namespace Api.Login.Services
{
    public interface IServiceLogin
    {
        Task<ResponseLogin> LoginAsync(RequestLogin requestLogin);
        Task<ResponseValidarUsuario> ValidarUsuarioAsync(ValidarUsuarioDto validarUsuario);
    }
}
