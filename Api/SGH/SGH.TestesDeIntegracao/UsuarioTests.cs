using Newtonsoft.Json;
using SGH.APi;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.TestesDeIntegracao.Config;
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
            var comando = new CriarUsuarioComando
            {
                Ativo = true,
                Email = "teste@gmail.com",
                Login = "teste",
                Nome = "Teste",
                PerfilCodigo = 1,
                Senha = "teste",
                Telefone = "37333333333"
            };

            var response = await _testsFixture.Client.PostAsync("/api/usuario/criar", _testsFixture.GerarCorpoRequisicao(comando));

            var responseString = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var usuarioCadastrado = JsonConvert.DeserializeObject<Usuario>(responseString);

            Assert.True(usuarioCadastrado.Codigo > 0);

        }

    }
}
