using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.AutoMapper
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
