using Dominio.ViewModel.CurriculoViewModel;
using Global;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Contratos
{
    public interface ICurriculoService : IServicoBase<CurriculoViewModel>
    {
        Task<Resposta<List<CurriculoDisciplinaViewModel>>> ListarDisciplinas(int codigoCurriculo);

    }
}
