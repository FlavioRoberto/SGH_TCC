using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
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

            CreateMap<DisciplinaViewModel, CurriculoDisciplinaPreRequisito>()
              .ForMember(d => d.CodigoCurriculoDisciplina, opt => opt.Ignore())
              .ForMember(d => d.CodigoDisciplina, opt => opt.MapFrom(p => p.Codigo))
              .ForMember(d => d.CurriculoDisciplina, opt => opt.Ignore())
              .ForMember(d => d.Disciplina, opt => opt.Ignore());

            CreateMap<CurriculoDisciplinaPreRequisito, DisciplinaViewModel>()
              .ForMember(d => d.Codigo, opt => opt.MapFrom(p => p.CodigoDisciplina))
              .ForMember(d => d.Descricao, opt => opt.MapFrom(p => p.Disciplina.Descricao))
              .ForMember(d => d.CodigoTipo, opt => opt.MapFrom(p => p.Disciplina.CodigoTipo));

            CreateMap<CriarCurriculoDisciplinaComando, CurriculoDisciplina>();

            CreateMap<CurriculoDisciplina, CriarCurriculoDisciplinaComando>();
        }
    }
}
