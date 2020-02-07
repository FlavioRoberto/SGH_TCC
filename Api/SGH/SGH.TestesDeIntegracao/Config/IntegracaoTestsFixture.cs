using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SGH.APi;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Implementacao.Autenticacao.Comandos.Login;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao.Config
{
    [CollectionDefinition(nameof(IntegracaoWebTestsFixtureCollection))]
    public class IntegracaoWebTestsFixtureCollection : ICollectionFixture<IntegracaoTestsFixture<StartupTests>>
    {

    }

    public class IntegracaoTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly AppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegracaoTestsFixture()
        {
            Factory = new AppFactory<TStartup>();
            Client = GerarClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        private HttpClient GerarClient()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            var client = Factory.CreateClient(clientOptions);

            var token = RecuperarToken(client).Result;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            return client;
        }

        internal HttpContent GerarCorpoRequisicao<T>(T conteudo)
        {
            return new StringContent(JsonConvert.SerializeObject(conteudo), Encoding.UTF8, "application/json");
        }

        private async Task<string> RecuperarToken(HttpClient client)
        {
            var comando = new LoginComando
            {
                Login = "admin",
                Senha = "admin"
            };

            var resultado = await client.PostAsync("/api/usuario/autenticar", GerarCorpoRequisicao(comando));

            var token = await resultado.Content.ReadAsStringAsync();

            return token;
        }

        internal async Task TestarRequisicaoInvalida(HttpResponseMessage response, string mensagemErro)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.Equal(mensagemErro.RemoverEspacosVazios(), responseString.RemoverEspacosVazios());
        }
    }
}
