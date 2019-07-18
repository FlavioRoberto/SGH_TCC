using AutoMapper;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel.CurriculoViewModel;
using Repositorio;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoDisciplinaServico : BaseService<CurriculoDisciplinaViewModel, CurriculoDisciplina>
    {
        public CurriculoDisciplinaServico(IRepositorio<CurriculoDisciplina> repositorio, IMapper mapper) : base(repositorio, mapper, "Disciplina do currículo")
        { }
    }
}
