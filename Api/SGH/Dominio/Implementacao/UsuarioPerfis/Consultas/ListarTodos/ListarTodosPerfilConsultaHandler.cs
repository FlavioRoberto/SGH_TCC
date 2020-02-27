using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Consultas.ListarTodos
{
    public class ListarTodosPerfilConsultaHandler : IRequestHandler<ListarTodosPerfilConsulta, List<UsuarioPerfil>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;

        public ListarTodosPerfilConsultaHandler(IUsuarioPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<UsuarioPerfil>> Handle(ListarTodosPerfilConsulta request, CancellationToken cancellationToken)
        {
            var perfis = await _repositorio.ListarTodos();
            return perfis;
        }
    }
}
