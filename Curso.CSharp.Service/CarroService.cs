using AutoMapper;
using Curso.CSharp.Models.Dtos;
using Curso.CSharp.Models.Entidades;
using Curso.CSharp.Models.Interfaces.Repository;
using Curso.CSharp.Models.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curso.CSharp.Service
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IMapper _mapper;

        public CarroService(ICarroRepository carroRepository, IMapper mapper)
        {
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public async Task<CarroDto> GetCarroAsync(long id)
        {
            var carro = await _carroRepository.GetCarroAsync(id);
            return carro == null ? null : _mapper.Map<CarroDto>(carro);
        }

        public async Task<CarroDto> GetFirstCarroAsync()
        {
            var carro = await _carroRepository.GetFirstCarroAsync();
            return carro == null ? null : _mapper.Map<CarroDto>(carro);
        }

        public async Task<List<CarroDto>> Paginado()
        {
            var carros = await _carroRepository.GetPaginateCarroAsync();
            return carros == null ? null : _mapper.Map<IEnumerable<Carro>, List<CarroDto>>(carros);
        }
    }
}
