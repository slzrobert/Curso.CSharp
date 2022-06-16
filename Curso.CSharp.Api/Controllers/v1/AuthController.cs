using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Curso.CSharp.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        public AuthController(){}

        [HttpPost("authorize")]
        public IActionResult Authorize()
        {
            var nome = "Robert";

            return Ok(nome);
        }
    }
}
