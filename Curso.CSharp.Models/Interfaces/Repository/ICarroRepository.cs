using Curso.CSharp.Models.Entidades;

namespace Curso.CSharp.Models.Interfaces.Repository
{
    public interface ICarroRepository
    {
        Task<Carro> GetCarroAsync(long id);
        Task<Carro> GetFirstCarroAsync();
        Task<List<Carro>> GetPaginateCarroAsync();
    }
}
