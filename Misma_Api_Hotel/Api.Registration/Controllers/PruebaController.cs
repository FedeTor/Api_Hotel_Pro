using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        [HttpGet("Saludar")]
        public async Task<string> Saludar(string salu)
        {

            return salu;
        }
    }
}
