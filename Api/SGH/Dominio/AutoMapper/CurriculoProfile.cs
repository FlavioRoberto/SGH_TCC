using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar;

namespace SGH.Dominio.Services.AutoMapper
{
    public class CurriculoProfile : Profile
    {
        public CurriculoProfile()
        {   
            CreateMap<Curriculo, CurriculoViewModel>()
              .ForMember(dto => dto.DescricaoCurso, opt => opt.MapFrom(p => p.Curso != null ? p.Curso.Descricao : ""));

            CreateMap<CurriculoViewModel, Curriculo>();
            //.ForMember(dto => dto.Disciplinas, opt => opt.Ignore());

            CreateMap<Paginacao<Curriculo>, Paginacao<CurriculoViewModel>>();

            CreateMap<Paginacao<CurriculoViewModel>, Paginacao<Curriculo>>();

            CreateMap<Curriculo, CriarCurriculoComando>();

            CreateMap<CriarCurriculoComando, Curriculo>();

            CreateMap<Curriculo, AtualizarCurriculoComando>();

            CreateMap<AtualizarCurriculoComando, Curriculo>();

        }
    }
}
