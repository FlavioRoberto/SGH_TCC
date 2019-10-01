using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Api.MapperProfiles
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
