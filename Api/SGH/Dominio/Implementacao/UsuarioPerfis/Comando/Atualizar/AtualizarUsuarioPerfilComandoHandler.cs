using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Comando.Atualizar
{
    public class AtualizarUsuarioPerfilComandoHandler : IRequestHandler<AtualizarUsuarioPerfilComando, Resposta<UsuarioPerfil>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;

        public AtualizarUsuarioPerfilComandoHandler(IUsuarioPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<UsuarioPerfil>> Handle(AtualizarUsuarioPerfilComando request, CancellationToken cancellationToken)
        {
            var usuarioPerfil = new UsuarioPerfil
            {
                Codigo = request.Codigo,
                Administrador = request.Administrador,
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Atualizar(usuarioPerfil);

            return new Resposta<UsuarioPerfil>(usuarioPerfil);
        }
    }
}
