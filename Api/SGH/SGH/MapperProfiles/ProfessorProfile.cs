using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;

namespace Api.MapperProfiles
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<Professor, ProfessorViewModel>();
            CreateMap<ProfessorViewModel, Professor>();
            CreateMap<Paginacao<Professor>, Paginacao<ProfessorViewModel>>();
            CreateMap<Paginacao<ProfessorViewModel>, Paginacao<Professor>>();
        }
    }
}
