using Bogus;
using Bogus.DataSets;
using SGH.APi;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.TestesDeIntegracao.Config;
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
        

        [Fact(DisplayName = "Realizar login com sucesso")]
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastro_DeveRealizarLoginComSucesso()
        {
            var comando = new LoginComando
            {
                Login = "admin",
                Senha = "admin"
            };

            var response = await _testsFixture.Client.PostAsync("/api/usuario/autenticar", _testsFixture.GerarCorpoRequisicao(comando));

            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(token);
        }

        [Fact(DisplayName = "Realizar login inválido")]
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastro_DeveRealizarLoginInválido()
        {
            var comando = new LoginComando();           

            var response = await _testsFixture.Client.PostAsync("/api/usuario/autenticar", _testsFixture.GerarCorpoRequisicao(comando));

            var mensagem = @"O campo de login não pode ser vazio.
                             O campo de senha não pode ser vazio.";

            await _testsFixture.TestarRequisicaoInvalida(response, mensagem);
        }

        [Fact(DisplayName = "Realizar login usuário ou senha inválidos")]
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastro_DeveRealizarLoginUsuarioOuSenhaInválido()
        {
            var comando = new LoginComando { 
                Login = "admin",
                Senha = "123"
            };

            var response = await _testsFixture.Client.PostAsync("/api/usuario/autenticar", _testsFixture.GerarCorpoRequisicao(comando));

            var mensagem = @"Usuário e/ou senha inválidos!";

            await _testsFixture.TestarRequisicaoInvalida(response, mensagem);
        }

        [Fact(DisplayName = "Realizar login usuário inativo")]
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastro_DeveRealizarLoginUsuarioInativo()
        {
            var comando = new LoginComando
            {
                Login = "inativo",
                Senha = "inativo"
            };

            var response = await _testsFixture.Client.PostAsync("/api/usuario/autenticar", _testsFixture.GerarCorpoRequisicao(comando));

            var mensagem = @"Não foi possível logar no sistema, o usuário informado está inativo!";

            await _testsFixture.TestarRequisicaoInvalida(response, mensagem);
        }

        [Fact(DisplayName = "Realizar cadastro com sucesso")]
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {

            var comando = GerarComando();

            var response = await _testsFixture.Client.PostAsync("/api/usuario/criar", _testsFixture.GerarCorpoRequisicao(comando));

            response.EnsureSuccessStatusCode();

            var usuarioCadastrado = await _testsFixture.RecuperarConteudoRequisicao<Usuario>(response);

            Assert.True(usuarioCadastrado.Codigo > 0);

        }

        [Fact(DisplayName = "Realizar cadastro usuário inválido")]
        [Trait("Integração", "Usuário")]
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
        [Trait("Integração", "Usuário")]
        public async Task Usuario_RealizarCadastroSemEmail_DeveRetornarMensagemUsuarioLoginJaCadastrado()
        {
            var comando = GerarComando();

            comando.Login = "admin";

            var mensagemErro = $"Já existe um usuário cadastrado com o login {comando.Login}.";

            await TestarCadastroUsuarioInvalido(mensagemErro, comando);

        }

        [Fact(DisplayName = "Realizar cadastro usuário e-mail já cadastrado")]
        [Trait("Integração", "Usuário")]
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

            await _testsFixture.TestarRequisicaoInvalida(response, mensagem);
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
