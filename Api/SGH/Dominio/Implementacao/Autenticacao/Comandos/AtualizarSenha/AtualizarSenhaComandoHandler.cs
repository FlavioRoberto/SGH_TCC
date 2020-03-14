using System.Threading.Tasks;
using MediatR;
using System.Threading;
using SGH.Dominio.Core;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Extensions;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComandoHandler : IRequestHandler<AtualizarSenhaComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IUsuarioResolverService _usuarioResolverService;
        private readonly IValidador<AtualizarSenhaComando> _validador;

        public AtualizarSenhaComandoHandler(IUsuarioRepositorio repositorio, IUsuarioResolverService usuarioResolverService, IValidador<AtualizarSenhaComando> validador)
        {
            _repositorio = repositorio;
            _usuarioResolverService = usuarioResolverService;
            _validador = validador;
        }

        public async Task<Resposta<string>> Handle(AtualizarSenhaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var codigoUsuarioLogado = _usuarioResolverService.GetUser().ToInt();

            var usuario = await _repositorio.Consultar(lnq => lnq.Codigo == codigoUsuarioLogado);

            usuario.Senha = request.NovaSenha.ToMD5();

            await _repositorio.Atualizar(usuario);

            return new Resposta<string>("A senha foi atualizada!");
        }
    }
}
