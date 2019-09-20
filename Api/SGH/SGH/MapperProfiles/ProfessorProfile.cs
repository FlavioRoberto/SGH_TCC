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
            CreateMap<Professor, ProfessorViewModel>()
                .ForMember(lnq => lnq.Cursos, lnq => lnq.MapFrom((src, dst) => dst.Cursos = 
                    src.Cursos != null ?
                    src.Cursos.Select(p => p.CursoId).ToList()
                : new List<int>()));

            CreateMap<ProfessorViewModel, Professor>()
                 .ForMember(lnq => lnq.Cursos, lnq => lnq.MapFrom((src, dst) => dst.Cursos = 
                     src.Cursos != null ?
                     src.Cursos.Select(p => new ProfessorCurso
                     {
                         CursoId = p,
                         ProfessorId = src.Codigo
                     }).ToList()
                 : new List<ProfessorCurso>()));

            CreateMap<Paginacao<ProfessorViewModel>, Paginacao<Professor>>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Paginacao<Professor>, Paginacao<ProfessorViewModel>>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
