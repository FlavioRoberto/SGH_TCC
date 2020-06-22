using FluentAssertions;
using Moq;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class AtualizarUsuarioComandoTeste
    {
        readonly UsuarioTestsFixture _usuarioTestsFixture;

        public AtualizarUsuarioComandoTeste(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }

        [Fact(DisplayName = "Atualizar usuário - Deve atualizar o usuário válido")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Atualizar_AtualizarUsuarioValido()
        {
            var usuarios = _usuarioTestsFixture.GerarUsuariosValidos(1, true);

            var comando = GerarUsuarioComando(usuarios.FirstOrDefault());

            var repositorioMock = new Mock<IUsuarioRepositorio>();

            repositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(Task.FromResult(true));

            repositorioMock.Setup(lnq => lnq.Atualizar(usuarios.FirstOrDefault())).Returns(Task.FromResult(usuarios.FirstOrDefault()));

            var validadorMock = new AtualizarUsuarioComandoValidador(repositorioMock.Object);

            var handler = new AtualizarUsuarioComandoHandler(repositorioMock.Object, validadorMock);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            repositorioMock.Verify(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>()), Times.Once);

            repositorioMock.Verify(c => c.Atualizar(It.IsAny<Usuario>()), Times.Once);
        }


        [Fact(DisplayName = "Atualizar usuário - Deve atualizar o usuário código igual a 0.")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Atualizar_AtualizarUsuarioCodigoInvalido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioInvalido();

            var mensagem = "O campo código não pode ser vazio.";

            await TestarUsuarioInvalido(usuario, mensagem);

        }

        [Fact(DisplayName = "Atualizar usuário - Deve atualizar o usuário código não encontrado.")]
        [Trait("Unitários", "Usuário")]
        public async Task Usuario_Atualizar_AtualizarUsuarioInvalido()
        {
            var usuario = _usuarioTestsFixture.GerarUsuarioInvalido();

            usuario.Codigo = 1;

            var mensagem = $"Não foi encontrado um usuário com o código {usuario.Codigo}.";

            await TestarUsuarioInvalido(usuario, mensagem);

        }

        private async Task TestarUsuarioInvalido(Usuario usuario, string mensagem)
        {
            var comando = GerarUsuarioComando(usuario);

            var repositorioMock = new Mock<IUsuarioRepositorio>();

            repositorioMock.Setup(c => c.Contem(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(Task.FromResult(false));

            var validadorMock = new AtualizarUsuarioComandoValidador(repositorioMock.Object);

            var handler = new AtualizarUsuarioComandoHandler(repositorioMock.Object, validadorMock);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            resultado.TemErro().Should().BeTrue();

            resultado.GetErros().RemoverEspacosVazios().Should().BeEquivalentTo(mensagem.RemoverEspacosVazios());
        }

        private AtualizarUsuarioComando GerarUsuarioComando(Usuario usuario)
        {
            return new AtualizarUsuarioComando
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
