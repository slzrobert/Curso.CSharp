using AutoMapper;
using Curso.CSharp.Repository.Dto;
using Curso.CSharp.Repository.Model;

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
