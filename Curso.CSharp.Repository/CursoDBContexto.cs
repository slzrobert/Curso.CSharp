using Curso.CSharp.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Curso.CSharp.Repository
{
    public class CursoDbContexto : DbContext
    {
        public CursoDbContexto(DbContextOptions<CursoDbContexto> options)
            : base(options)
        {
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Marca> Marcas { get; set; }
    }
}
