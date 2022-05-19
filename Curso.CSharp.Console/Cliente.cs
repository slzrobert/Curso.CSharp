namespace Curso.CSharp.ConsoleTeste
{
    public class Cliente
    {
        public SexoEnum Sexo { get; set; }

        public Cliente(SexoEnum sexo)
        {
            Sexo = sexo;
        }

        public override string ToString()
        {
            return $"{Sexo}";
        }
    }
}
