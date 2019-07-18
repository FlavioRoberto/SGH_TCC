using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Repositorio;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : BaseService<DisciplinaViewModel, Disciplina>
    {
        public DisciplinaServico(IRepositorio<Disciplina> repositorio, IMapper mapper) : base(repositorio, mapper, "Disciplina")
        { }
        
    }
}
