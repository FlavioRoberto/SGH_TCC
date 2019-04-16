using Dominio.ViewModel;
using Global;
using System.Collections.Generic;

namespace Servico.Contratos
{
    public interface IServicoBase<T> where T : class
    {

        Resposta<T> Criar(T entidade);
        Resposta<T> ListarPeloId(long id);
        Resposta<List<T>> ListarTodos();
        Resposta<Paginacao<T>> ListarComPaginacao(Paginacao<T> entidade);
        Resposta<T> Atualizar(T entidade);
        Resposta<bool> Remover(long id);

    }
}
