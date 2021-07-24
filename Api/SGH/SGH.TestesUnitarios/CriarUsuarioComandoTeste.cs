using FluentAssertions;
using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MediatR;
using SGH.Dominio.Core.Events;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class CriarUsuarioComandoTeste
    {
        readonly UsuarioTestsFixture _usuarioTestsFixture;

        public CriarUsuarioComandoTeste(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }
     
        [Fact(DisplayName = "Criar usuário - Deve cadastrar o usuário válido")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Criar_CadastrarUsuarioValido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioValido();

            var comando = GerarUsuarioComando(usuario);

            var mocker = new AutoMocker();

            var handler = mocker.CreateInstance<CriarUsuarioComandoHandler>();

            mocker.GetMock<IValidador<CriarUsuarioComando>>().Setup(c => c.Validate(comando)).Returns(new ValidationResult());

            mocker.GetMock<IUsuarioRepositorio>().Setup(c => c.Criar(usuario)).Returns(Task.FromResult(usuario));

            var assunto = handler.GerarEmailAssunto();

            var mensagem = handler.GerarEmailMensagem(usuario.Login, usuario.Senha);

            mocker.GetMock<IMediator>().Setup(c => c.Send(It.IsAny<EnviarEmailEvent>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(Unit.Value));

            var resultado = await handler.Handle(comando, CancellationToken.None);

            mocker.GetMock<IUsuarioRepositorio>().Verify(c => c.Criar(It.IsAny<Usuario>()), Times.Once);

            mocker.GetMock<IMediator>().Verify(c => c.Send(It.IsAny<EnviarEmailEvent>(), It.IsAny<CancellationToken>()), Times.Once);

            resultado.Should().NotBeNull();
            
        }

        [Fact(DisplayName = "Criar usuário - Deve retornar mensagens ao cadastrar usuário inválido")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Criar_CadastrarUsuarioInvalido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioInvalido();

            var comando = GerarUsuarioComando(usuario);

            var repositorioMock = new Mock<IUsuarioRepositorio>();

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            repositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(Task.FromResult(true));

            cursoRepositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Curso, bool>>>())).Returns(Task.FromResult(false));

            var emailServiceMock = new Mock<IMediator>();
            
            var validadorMock = new CriarUsuarioComandoValidador(repositorioMock.Object, cursoRepositorioMock.Object);

            var handler = new CriarUsuarioComandoHandler(repositorioMock.Object,validadorMock,emailServiceMock.Object);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            var mensagemErro = @$"O campo de e-mail não pode ser vazio.
                                 O campo de login não pode ser vazio.
                                 O campo de nome não pode ser vazio.
                                 O campo de perfil não pode ser vazio.
                                 Já existe um usuário cadastrado com o e-mail {comando.Email}.
                                 Já existe um usuário cadastrado com o login {comando.Login}.";

            resultado.TemErro().Should().BeTrue();

            resultado.GetErros().RemoverEspacosVazios().Should().BeEquivalentTo(mensagemErro.RemoverEspacosVazios());

        }

        [Fact(DisplayName = "Criar usuário - Deve retornar validação somente usuário coordenador possui curso vinculado")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Criar_DeveRetornarValidacaoSomenteUsuarioCoordenadorPossuiCurso()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioValido();

            usuario.PerfilCodigo = 1;

            usuario.CursoCodigo = 1;

            var comando = GerarUsuarioComando(usuario);

            var repositorioMock = new Mock<IUsuarioRepositorio>();

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            repositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(Task.FromResult(false));

            cursoRepositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Curso, bool>>>())).Returns(Task.FromResult(true));

            var emailServiceMock = new Mock<IMediator>();

            var validadorMock = new CriarUsuarioComandoValidador(repositorioMock.Object, cursoRepositorioMock.Object);

            var handler = new CriarUsuarioComandoHandler(repositorioMock.Object, validadorMock, emailServiceMock.Object);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            var mensagemErro = @$"Somente usuários com perfil coordenador de curso podem ter cursos vinculados.";

            resultado.TemErro().Should().BeTrue();

            resultado.GetErros().RemoverEspacosVazios().Should().BeEquivalentTo(mensagemErro.RemoverEspacosVazios());

        }

        private CriarUsuarioComando GerarUsuarioComando(Usuario usuario)
        {
            return new CriarUsuarioComando
            {
                Ativo = usuario.Ativo,
                Codigo = usuario.Codigo,
                CursoCodigo = usuario.CursoCodigo,
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
