using AutoMapper;
using Curso.CSharp.Models.Dtos;
using Curso.CSharp.Models.Entidades;

namespace Curso.CSharp.Repository.Mappings
{
    public class CarroMapping : Profile 
    {
        public CarroMapping()
        {
            CreateMap<Carro, CarroDto>();
        }
    }
}
