using Moq;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Implementacao.Autenticacao.Comandos.Login;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class AutenticacaoTests
    {
        readonly UsuarioTestsFixture _usuarioTestsFixture;

        public AutenticacaoTests(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }

        [Fact(DisplayName = "Login - Deve retornar o token")]
        [Trait("Categoria", "Autenticação")]
        public async Task Autenticacao_login_retornarToken()
        {
            var usuarios = _usuarioTestsFixture.GerarUsuariosValidos(3, true);

            var comando = new LoginComando
            {
                Login = usuarios.FirstOrDefault().Login,
                Senha = usuarios.FirstOrDefault().Senha
            };

            var usuarioRepositorio = new Mock<IUsuarioRepositorio>();
            
            usuarioRepositorio.Setup(c => c.RetornarUsuarioPorLoginESenha(comando.Login, comando.Senha)).Returns(Task.FromResult(usuarios.FirstOrDefault()));

            var validador = new LoginComandoValidator(usuarioRepositorio.Object);

            var handler = new LoginComandoHandler(usuarioRepositorio.Object, validador);

            #region automock
            //var mocker = new AutoMocker();

            //var handler = mocker.CreateInstance<LoginComandoHandler>();

            //mocker.GetMock<IUsuarioRepositorio>().Setup(c => c.RetornarUsuarioPorLoginESenha(comando.Login, comando.Senha)).Returns(Task.FromResult(usuarios.FirstOrDefault()));

            //mocker.GetMock<ILoginComandoValidator>().Setup(c => c.Validate(comando)).Returns(new ValidationResult());
            #endregion

            var resultado = await handler.Handle(comando, CancellationToken.None);

            Assert.False(resultado.TemErro());
        }
    }
}
