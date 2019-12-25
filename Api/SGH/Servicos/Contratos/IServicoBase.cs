using Dominio.ViewModel;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Contratos
{
    public interface IServicoBase<T> where T : class
    {

        Task<Resposta<T>> Criar(T entidade);
        Task<Resposta<List<T>>> ListarTodos();
        Task<Resposta<Paginacao<T>>> ListarComPaginacao(Paginacao<T> entidade);
        Task<Resposta<T>> Atualizar(T entidade);
        Task<Resposta<bool>> Remover(long id);
    }
}
