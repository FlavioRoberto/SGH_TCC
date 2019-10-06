using AutoMapper;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Dominio.ViewModel.CurriculoViewModel;
using Dominio.ViewModel.DisciplinaViewModel;

namespace Api.MapperProfiles
{
    public class CurriculoProfile : Profile
    {
        public CurriculoProfile()
        {
            CreateMap<DisciplinaViewModel, CurriculoDisciplinaPreRequisito>()
                .ForMember(d => d.CodigoCurriculoDisciplina, opt => opt.Ignore())
                .ForMember(d => d.CodigoDisciplina, opt => opt.MapFrom(p => p.Codigo))
                .ForMember(d => d.CurriculoDisciplina, opt => opt.Ignore())
                .ForMember(d => d.Disciplina, opt => opt.Ignore());

            CreateMap<CurriculoDisciplinaPreRequisito, DisciplinaViewModel>()
              .ForMember(d => d.Codigo, opt => opt.MapFrom(p => p.CodigoDisciplina))
              .ForMember(d => d.Descricao, opt => opt.MapFrom(p => p.Disciplina.Descricao))
              .ForMember(d => d.CodigoTipo, opt => opt.MapFrom(p => p.Disciplina.CodigoTipo));

            CreateMap<CurriculoDisciplinaViewModel, CurriculoDisciplina>().
                ForMember(d => d.CurriculoDisciplinaPreRequisito, opt => opt.MapFrom(p => p.PreRequisitos));

            CreateMap<CurriculoDisciplina, CurriculoDisciplinaViewModel>().
                ForMember(d => d.PreRequisitos, opt => opt.MapFrom(p => p.CurriculoDisciplinaPreRequisito));

            CreateMap<Curriculo, CurriculoViewModel>()
              .ForMember(dto => dto.DescricaoCurso, opt => opt.MapFrom(p => p.Curso != null ? p.Curso.Descricao : ""));

            CreateMap<CurriculoViewModel, Curriculo>();
            //.ForMember(dto => dto.Disciplinas, opt => opt.Ignore());

            CreateMap<Paginacao<Curriculo>, Paginacao<CurriculoViewModel>>();

            CreateMap<Paginacao<CurriculoViewModel>, Paginacao<Curriculo>>();

        }
    }
}
