using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.ViewModel;
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
         
            CreateMap<CargoDisciplina, CriarCargoDisciplinaComando>().ReverseMap();

            CreateMap<CargoDisciplinaListarPorCurriculoViewModel, CargoDisciplina>();
            CreateMap<CargoDisciplina, CargoDisciplinaListarPorCurriculoViewModel>()
             .ForMember(dto => dto.Descricao, opt => opt.MapFrom(c => string.IsNullOrEmpty(c.Descricao) ? c.Disciplina.Disciplina.Descricao : c.Descricao))
             .ForMember(dto => dto.Professor, opt => opt.MapFrom(c => $"Cargo {c.Cargo.Numero}"));

            CreateMap<CargoDisciplina, EditarCargoDisciplinaComando>().ReverseMap();

        }
    }
}
