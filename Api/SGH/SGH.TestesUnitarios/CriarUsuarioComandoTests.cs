using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core.Email;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class CriarUsuarioComandoTests
    {
        readonly UsuarioTestsFixture _usuarioTestsFixture;

        public CriarUsuarioComandoTests(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }
     
        [Fact(DisplayName = "Criar usuário - Deve cadastrar o usuário válido")]
        [Trait("Categoria", "Usuário")]
        public async Task Usuario_Criar_CadastrarUsuarioValido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioValido();

            var comando = GerarUsuarioComando(usuario);

            var mocker = new AutoMocker();

            var handler = mocker.CreateInstance<CriarUsuarioComandoHandler>();

            mocker.GetMock<ICriarUsuarioComandoValidador>().Setup(c => c.Validate(comando)).Returns(new ValidationResult());

            mocker.GetMock<IUsuarioRepositorio>().Setup(c => c.Criar(usuario)).Returns(Task.FromResult(usuario));

            var assunto = handler.GerarEmailAssunto();

            var mensagem = handler.GerarEmailMensagem(usuario.Login, usuario.Senha);

            mocker.GetMock<IEmailService>().Setup(c => c.Enviar(usuario.Email, assunto, mensagem)).Returns(Task.CompletedTask);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            mocker.GetMock<IUsuarioRepositorio>().Verify(c => c.Criar(It.IsAny<Usuario>()), Times.Once);

            mocker.GetMock<IEmailService>().Verify(c => c.Enviar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            
            Assert.NotNull(resultado);

        }

        [Fact(DisplayName = "Criar usuário - Deve cadastrar o usuário inválido")]
        [Trait("Categoria", "Usuário")]
        public async Task Usuario_Criar_CadastrarUsuarioInvalido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioInvalido();

            var comando = GerarUsuarioComando(usuario);

            var repositorioMock = new Mock<IUsuarioRepositorio>();

            repositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(Task.FromResult(true));

            var emailServiceMock = new Mock<IEmailService>();
            
            var validadorMock = new CriarUsuarioComandoValidador(repositorioMock.Object);

            var handler = new CriarUsuarioComandoHandler(repositorioMock.Object,validadorMock,emailServiceMock.Object);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            var mensagemErro = @$"O campo de e-mail não pode ser vazio.
                                 O campo de login não pode ser vazio.
                                 O campo de nome não pode ser vazio.
                                 O campo de perfil não pode ser vazio.
                                 Já existe um usuário cadastrado com o e-mail {comando.Email}.
                                 Já existe um usuário cadastrado com o login {comando.Login}.";

            Assert.True(resultado.TemErro());

            Assert.Equal(mensagemErro.RemoverEspacosVazios(), resultado.GetErros().RemoverEspacosVazios());

        }

        private CriarUsuarioComando GerarUsuarioComando(Usuario usuario)
        {
            return new CriarUsuarioComando
            {
                Ativo = usuario.Ativo,
                Codigo = usuario.Codigo,
                Email = usuario.Email,
                Foto = usuario.Foto,
                Login = usuario.Login,
                Nome = usuario.Nome,
                PerfilCodigo = usuario.PerfilCodigo,
                Senha = usuario.Senha,
                Telefone = usuario.Telefone
            };
        }

    }
}
