using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Professores.Comandos.Criar;

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
