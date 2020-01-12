using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Consultas.ListarTodos
{
    public class ListarTodosUsuarioConsultaHandler : IRequestHandler<ListarTodosUsuarioConsulta, List<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public ListarTodosUsuarioConsultaHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Usuario>> Handle(ListarTodosUsuarioConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarTodos();
        }
    }
}
