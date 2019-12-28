using Global;
using Repositorio.Contratos;
using Servico.Extensions;
using Servico.Implementacao.Autenticacao.Contratos;
using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComandoHandler : IAutenticacaoService
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ILoginComandoValidator _validador;

        public LoginComandoHandler(IUsuarioRepositorio repositorio, ILoginComandoValidator validator)
        {
            _repositorio = repositorio;
            _validador = validator;
        }

        public async Task<Resposta<string>> Autenticar(LoginComando login)
        {
            var erros = _validador.Validar(login);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var usuario = await _repositorio.RetornarUsuarioPorLoginESenha(login.Login, login.Senha);

            string token = TokenGeradorHelper.Gerar(usuario);

            return new Resposta<string>(token);
        }
    }
}
