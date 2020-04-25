using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class CargoDisciplinaProfile : Profile
    {
        public CargoDisciplinaProfile()
        {
            CreateMap<CargoDisciplinaViewModel, CargoDisciplina>();
            CreateMap<CargoDisciplina, CargoDisciplinaViewModel>()
                .ForMember(dto => dto.CursoDescricao, opt => opt.MapFrom(c => c.Disciplina.Curriculo.Curso.Descricao))
                .ForMember(dto => dto.Descricao, opt => opt.MapFrom(c => string.IsNullOrEmpty(c.Descricao) ? c.Disciplina.Disciplina.Descricao : c.Descricao));
            CreateMap<CargoDisciplina, CriarCargoDisciplinaComando>();
            CreateMap<CriarCargoDisciplinaComando, CargoDisciplina>();
        }
    }
}
