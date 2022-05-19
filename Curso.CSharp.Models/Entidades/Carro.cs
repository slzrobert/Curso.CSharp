namespace Curso.CSharp.Models.Entidades
{
    public class Carro
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long MarcaId { get; set; }
        public Marca Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
    }
}
