using AutoMapper;
using Domain.Dtos.Livro;
using Domain.Entities.Livros;
using Domain.Entities.Views;

namespace Infra.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LivroView, LivroFlatDTO>().ReverseMap();



            CreateMap<Livro, LivroFlatDTO>().ReverseMap();
            CreateMap<RelatorioLivro, RelatorioDTO>().ReverseMap();

            CreateMap<Autor, AutorFlatDto>()
                .ForMember(dest => dest.CodAu, opt => opt.MapFrom(src => src.CodAu))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Assunto, AssuntoFlatDto>()
                .ForMember(dest => dest.CodAs, opt => opt.MapFrom(src => src.CodAs))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
