using AutoMapper;
using Curso.CSharp.Models.Dtos;
using Curso.CSharp.Models.Entidades;

namespace Curso.CSharp.Repository.Mappings
{
    public class MarcaMapping : Profile
    {
        public MarcaMapping()
        {
            CreateMap<Marca, MarcaDto>();
        }
    }
}
