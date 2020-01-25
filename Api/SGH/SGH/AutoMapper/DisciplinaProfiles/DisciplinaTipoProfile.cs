using AutoMapper;
using SGH.APi.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Api.AutoMapper
{
    public class DisciplinaTipoProfile : Profile
    {
        public DisciplinaTipoProfile()
        {
            CreateMap<DisciplinaTipo, DisciplinaTipoViewModel>();
            CreateMap<Paginacao<DisciplinaTipo>, Paginacao<DisciplinaTipoViewModel>>();
            CreateMap<DisciplinaTipoViewModel, DisciplinaTipo>();
            CreateMap<Paginacao<DisciplinaTipoViewModel>, Paginacao<DisciplinaTipo>>();
        }
    }
}
