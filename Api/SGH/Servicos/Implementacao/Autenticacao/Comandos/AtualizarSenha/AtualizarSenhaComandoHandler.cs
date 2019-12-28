using Servico.Contratos;
using System.Threading.Tasks;
using Global.Extensions;
using Global;
using Repositorio.Contratos;
using Servico.Implementacao.Autenticacao.Contratos;
using Servico.Extensions;

namespace Servico.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComandoHandler : IAtualizarSenhaService
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IUsuarioResolverService _usuarioResolverService;
        private readonly IAtualizarSenhaComandoValidador _validador;

        public AtualizarSenhaComandoHandler(IUsuarioRepositorio repositorio, IUsuarioResolverService usuarioResolverService, IAtualizarSenhaComandoValidador validador)
        {
            _repositorio = repositorio;
            _usuarioResolverService = usuarioResolverService;
            _validador = validador;
        }

        public async Task<Resposta<string>> Atualizar(AtualizarSenhaComando comando)
        {
            var erros = _validador.Validar(comando);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var codigoUsuarioLogado = _usuarioResolverService.GetUser().ToInt();

            var usuario = await _repositorio.Listar(lnq => lnq.Codigo == codigoUsuarioLogado);

            usuario.Senha = comando.NovaSenha.ToMD5();

            await _repositorio.Atualizar(usuario);

            return new Resposta<string>("A senha foi atualizada!");
        }
    }
}
