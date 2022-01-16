using Curso.CSharp.Repository.Dto;
using Curso.CSharp.Repository.Model;
using Curso.CSharp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Curso.CSharp.Api
{
    [Route("api/carros")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly CarroService _carroService;

        public CarroController(CarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]
        public async Task<ActionResult<CarroDto>> PrimeiroCarroAsync()
        {
            return await _carroService.GetFirstCarroAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarroDto>> BuscarCarroAsync(long id)
        {
            CarroDto car = await _carroService.GetCarroAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
    }
}
