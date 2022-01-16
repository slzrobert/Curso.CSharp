using Curso.CSharp.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Curso.CSharp.Repository
{
    public class CarroRepository
    {
        private readonly CursoDBContexto _dbContext;

        public CarroRepository(CursoDBContexto cursoDBContexto)
        {
            _dbContext = cursoDBContexto;
        }

        public async Task<Carro> GetCarroAsync(long id)
        {
            return await _dbContext.Carros.Include(c => c.Marca).SingleAsync(c => c.Id.Equals(id));
        }

        public async Task<Carro> GetFirstCarroAsync()
        {
            return await _dbContext.Carros.Include(c => c.Marca).FirstAsync();
        }
    }
}
