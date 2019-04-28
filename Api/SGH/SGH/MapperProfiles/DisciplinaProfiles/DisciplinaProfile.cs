using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;

namespace Api.MapperProfiles.DisciplinaProfiles
{
    public class DisciplinaProfile : Profile
    {
        public DisciplinaProfile()
        {
            CreateMap<Disciplina, DisciplinaViewModel>();
            CreateMap<Paginacao<Disciplina>, Paginacao<DisciplinaViewModel>>();
            CreateMap<DisciplinaViewModel, Disciplina>();
            CreateMap<Paginacao<DisciplinaViewModel>, Paginacao<Disciplina>>();
        }
    }
}
