using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, Resposta<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ICriarUsuarioComandoValidador _validador;

        public CriarUsuarioComandoHandler(IUsuarioRepositorio repositorio, ICriarUsuarioComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Usuario>> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Usuario>(erros);

            var usuario = new Usuario
            {
                Ativo = request.Ativo,
                Email = request.Email,
                Foto = request.Foto,
                Login = request.Login,
                Nome = request.Nome,
                PerfilCodigo = request.PerfilCodigo,
                Telefone = request.Telefone
            };

            var usuarioCadastrado = await _repositorio.Criar(usuario);

            return new Resposta<Usuario>(usuarioCadastrado);

        }
    }
}
