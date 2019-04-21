using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;

namespace Api.MapperProfiles
{
    public class CursoProfile : Profile
    {
        public CursoProfile()
        {
            CreateMap<Curso, CursoViewModel>();
            CreateMap<Paginacao<Curso>, Paginacao<CursoViewModel>>();
            CreateMap<CursoViewModel, Curso>();
            CreateMap<Paginacao<CursoViewModel>, Paginacao<Curso>>();
        }
    }
}
