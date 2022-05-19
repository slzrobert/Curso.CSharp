namespace Curso.CSharp.Models.Entidades
{
    public class Marca
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Carro> Carros { get; set; }
    }
}
