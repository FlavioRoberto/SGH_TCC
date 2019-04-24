using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;

namespace Api.MapperProfiles.DisciplinaProfiles
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
