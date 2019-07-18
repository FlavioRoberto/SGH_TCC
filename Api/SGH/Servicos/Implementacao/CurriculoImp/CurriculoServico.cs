using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel.CurriculoViewModel;
using Repositorio;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoServico : BaseService<CurriculoViewModel, Curriculo>
    {
        public CurriculoServico(IRepositorio<Curriculo> repositorio, IMapper mapper) : base(repositorio, mapper, "Currículo")
        { }
      
    }
}
