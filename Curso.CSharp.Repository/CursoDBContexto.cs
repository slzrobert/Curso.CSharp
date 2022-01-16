using Curso.CSharp.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace Curso.CSharp.Repository
{
    public class CursoDBContexto : DbContext
    {
        public CursoDBContexto(DbContextOptions<CursoDBContexto> options)
            : base(options)
        {
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Marca> Marcas { get; set; }
    }
}
