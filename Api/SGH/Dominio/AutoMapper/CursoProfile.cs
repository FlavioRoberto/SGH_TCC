using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.AutoMapper
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
