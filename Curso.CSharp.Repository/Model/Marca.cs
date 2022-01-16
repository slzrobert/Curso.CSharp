using System.Collections.Generic;

namespace Curso.CSharp.Repository.Model
{
    public class Marca
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Carro> Carros { get; set; }
    }
}