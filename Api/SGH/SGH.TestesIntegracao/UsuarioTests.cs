using Newtonsoft.Json;
using SGH.Api;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
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

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/usuario/criar")
            {
                Content = new StringContent(JsonConvert.SerializeObject(comando))
            };

            var response = await _testsFixture.Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();


        }

    }
}
