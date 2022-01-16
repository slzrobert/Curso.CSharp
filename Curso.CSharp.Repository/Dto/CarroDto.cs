namespace Curso.CSharp.Repository.Dto
{
    public class CarroDto
    {
        public  long Id { get; set; }
        public string Nome { get; set; }

        public MarcaDto Marca { get; set; }
    }
}
