using Curso.CSharp.Models.Dtos;

namespace Curso.CSharp.Models.Interfaces.Service
{
    public interface ICarroService
    {
        Task<CarroDto> GetCarroAsync(long id);
        Task<CarroDto> GetFirstCarroAsync();
        Task<List<CarroDto>> Paginado();
    }
}
