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
                .ForMember(dto => dto.disciplinaDescricao, opt => opt.MapFrom(c => c.Disciplina.Disciplina.Descricao))
                .ForMember(dto => dto.cursoDescricao, opt => opt.MapFrom(c => c.Disciplina.Curriculo.Curso.Descricao));
            CreateMap<CargoDisciplina, CriarCargoDisciplinaComando>();
            CreateMap<CriarCargoDisciplinaComando, CargoDisciplina>();
        }
    }
}
