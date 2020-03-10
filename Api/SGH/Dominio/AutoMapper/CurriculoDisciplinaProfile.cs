using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar;
using SGH.Dominio.Services.ViewModel;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class CurriculoDisciplinaProfile : Profile
    {
        public CurriculoDisciplinaProfile()
        {
            CreateMap<CurriculoDisciplinaViewModel, CurriculoDisciplina>().
              ForMember(d => d.CurriculoDisciplinaPreRequisito, opt => opt.MapFrom(p => p.PreRequisitos));

            CreateMap<CurriculoDisciplina, CurriculoDisciplinaViewModel>().
                ForMember(d => d.PreRequisitos, opt => opt.MapFrom(p => p.CurriculoDisciplinaPreRequisito));

            CreateMap<DisciplinCurriculoPreRequisitoaViewModel, CurriculoDisciplinaPreRequisito>()
              .ForMember(d => d.CodigoDisciplina, opt => opt.MapFrom(p => p.CodigoDisciplina))
              .ForMember(d => d.CurriculoDisciplina, opt => opt.Ignore())
              .ForMember(d => d.Disciplina, opt => opt.Ignore());

            CreateMap<CurriculoDisciplinaPreRequisito, DisciplinCurriculoPreRequisitoaViewModel>()
              .ForMember(d => d.CodigoDisciplina, opt => opt.MapFrom(p => p.CodigoDisciplina))
              .ForMember(d => d.DescricaoDisciplina, opt => opt.MapFrom(p => p.Disciplina.Descricao))
              .ForMember(d => d.CodigoTipo, opt => opt.MapFrom(p => p.Disciplina.CodigoTipo));

            CreateMap<CriarCurriculoDisciplinaComando, CurriculoDisciplina>();

            CreateMap<CurriculoDisciplina, CriarCurriculoDisciplinaComando>();

            CreateMap<CurriculoDisciplina, EditarCurriculoDisciplinaComando>();

            CreateMap<EditarCurriculoDisciplinaComando, CurriculoDisciplina>();
        }
    }
}
