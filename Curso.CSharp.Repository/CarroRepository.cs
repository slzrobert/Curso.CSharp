using Curso.CSharp.Models.Entidades;
using Curso.CSharp.Models.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.CSharp.Repository
{
    public class CarroRepository : ICarroRepository
    {
        private readonly CursoDbContexto _dbContext;

        public CarroRepository(CursoDbContexto cursoDbContexto)
        {
            _dbContext = cursoDbContexto;
        }

        public async Task<Carro> GetCarroAsync(long id)
        {
            return await _dbContext.Carros.Include(a => a.Marca).Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Carro> GetFirstCarroAsync()
        {
            return await _dbContext.Carros.Include(c => c.Marca).FirstAsync();
        }

        public async Task<List<Carro>> GetPaginateCarroAsync()
        {
            var pageSize = 3;
            var pageNumber = 1;
            return await _dbContext.Carros.Include(c => c.Marca).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
