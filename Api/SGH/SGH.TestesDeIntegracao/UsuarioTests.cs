using Bogus;
using Bogus.DataSets;
using Newtonsoft.Json;
using SGH.APi;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.TestesDeIntegracao.Config;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public UsuarioTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro com sucesso")]
        [Trait("Categoria", "Integração Api - Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {

            var comando = GerarComando();

            var response = await _testsFixture.Client.PostAsync("/api/usuario/criar", _testsFixture.GerarCorpoRequisicao(comando));

            var responseString = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var usuarioCadastrado = JsonConvert.DeserializeObject<Usuario>(responseString);

            Assert.True(usuarioCadastrado.Codigo > 0);

        }

        [Fact(DisplayName = "Realizar cadastro usuário inválido")]
        [Trait("Categoria", "Integração Api - Usuário")]
        public async Task Usuario_RealizarCadastroSemEmail_DeveRetornarMensagemUsuarioInvalido()
        {
            var comando = new CriarUsuarioComando();

            var mensagemErro = @"O campo de e-mail não pode ser vazio.
                                 O campo de login não pode ser vazio.
                                 O campo de nome não pode ser vazio.
                                 O campo de perfil não pode ser vazio.";

            await TestarCadastroUsuarioInvalido(mensagemErro, comando);

        }

        [Fact(DisplayName = "Realizar cadastro usuário login já cadastrado")]
        [Trait("Categoria", "Integração Api - Usuário")]
        public async Task Usuario_RealizarCadastroSemEmail_DeveRetornarMensagemUsuarioLoginJaCadastrado()
        {
            var comando = GerarComando();

            comando.Login = "admin";

            var mensagemErro = $"Já existe um usuário cadastrado com o login {comando.Login}.";

            await TestarCadastroUsuarioInvalido(mensagemErro, comando);

        }

        [Fact(DisplayName = "Realizar cadastro usuário e-mail já cadastrado")]
        [Trait("Categoria", "Integração Api - Usuário")]
        public async Task Usuario_RealizarCadastroSemEmail_DeveRetornarMensagemUsuarioEmailJaCadastrado()
        {
            var comando = GerarComando();

            comando.Email = "admin@gmail.com";

            var mensagemErro = $"Já existe um usuário cadastrado com o e-mail {comando.Email}.";

            await TestarCadastroUsuarioInvalido(mensagemErro, comando);

        }

        private async Task TestarCadastroUsuarioInvalido(string mensagem, CriarUsuarioComando comando)
        {
            var response = await _testsFixture.Client.PostAsync("/api/usuario/criar", _testsFixture.GerarCorpoRequisicao(comando));

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.Equal(mensagem.RemoverEspacosVazios(), responseString.RemoverEspacosVazios());
        }

        private CriarUsuarioComando GerarComando()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var nome = new Faker().Name.FirstName(genero);

            return new CriarUsuarioComando
            {
                Ativo = true,
                Email = new Faker().Internet.Email(nome),
                Login = new Faker().Internet.UserName(nome),
                Nome = nome,
                PerfilCodigo = 1,
                Senha = new Faker().Internet.Password(),
                Telefone = new Faker().Phone.PhoneNumber()
            };

        }
    }
}
