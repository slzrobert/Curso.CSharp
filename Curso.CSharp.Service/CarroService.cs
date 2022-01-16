using AutoMapper;
using Curso.CSharp.Repository;
using Curso.CSharp.Repository.Dto;
using Curso.CSharp.Repository.Model;
using System.Threading.Tasks;

namespace Curso.CSharp.Service
{
    public class CarroService
    {
        private readonly CarroRepository _carroRepository;
        private readonly IMapper _mapper;

        public CarroService(CarroRepository carroRepository, IMapper mapper)
        {
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public async Task<CarroDto> GetCarroAsync(long id)
        {
            Carro carro = await _carroRepository.GetCarroAsync(id);
            return _mapper.Map<CarroDto>(carro);
        }

        public async Task<CarroDto> GetFirstCarroAsync()
        {
            Carro carro = await _carroRepository.GetFirstCarroAsync();
            return _mapper.Map<CarroDto>(carro);
        }
    }
}
