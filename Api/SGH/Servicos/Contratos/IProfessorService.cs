using Dominio.ViewModel;
using Global;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Contratos
{
    public interface IProfessorService : IServicoBase<ProfessorViewModel>
    {
        Task<Resposta<List<ProfessorViewModel>>> ListarAtivos();

    }
}
