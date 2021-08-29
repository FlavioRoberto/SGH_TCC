using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Comando.Criar
{
    public class CriarUsuarioPerfilComandoHandler : IRequestHandler<CriarUsuarioPerfilComando, Resposta<UsuarioPerfil>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;

        public CriarUsuarioPerfilComandoHandler(IUsuarioPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<UsuarioPerfil>> Handle(CriarUsuarioPerfilComando request, CancellationToken cancellationToken)
        {
            var usuarioPerfil = new UsuarioPerfil
            {
                Administrador = request.Administrador,
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Criar(usuarioPerfil);

            return new Resposta<UsuarioPerfil>(resultado);
        }
    }
}
