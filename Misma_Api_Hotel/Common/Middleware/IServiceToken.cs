using System.Threading.Tasks;

namespace Common.Middleware
{
    public interface IServiceToken
    {
        Task<bool> ValidarTokenAsync(string token);
        Task<bool> CancelarTokenAsync(string token);
    }
}
