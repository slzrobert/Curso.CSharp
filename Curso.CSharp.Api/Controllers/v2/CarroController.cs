using Curso.CSharp.Api.Models;
using Curso.CSharp.Models.Dtos;
using Curso.CSharp.Models.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Curso.CSharp.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _carroService;

        public CarroController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<CarroDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CarroDto>> PrimeiroCarroAsync()
        {
            return await _carroService.GetFirstCarroAsync();
        }

        /// <summary>
        /// Buscar um carro especifico.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<CarroDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CarroDto>> BuscarCarroAsync(long id)
        {
            CarroDto car = await _carroService.GetCarroAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        /// <summary>
        /// Lista paginada de carros.
        /// </summary>
        [HttpGet("lista")]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<CarroDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CarroDto>>> Paginado()
        {
            //throw new ApiException("Erro ao Paginar Resultados.");

            List<CarroDto> cars = await _carroService.Paginado();
            if (cars == null)
            {
                return NotFound();
            }
            return cars;
        }
    }
}
