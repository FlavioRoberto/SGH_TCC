using AutoMapper;
using Dominio.Model;
using SGH.APi.ViewModel;
using SGH.Dominio.Model;

namespace SGH.Api.AutoMapper
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<Professor, ProfessorViewModel>();

            CreateMap<ProfessorViewModel, Professor>();

            CreateMap<Paginacao<ProfessorViewModel>, Paginacao<Professor>>();

            CreateMap<Paginacao<Professor>, Paginacao<ProfessorViewModel>>();

        }
    }
}
