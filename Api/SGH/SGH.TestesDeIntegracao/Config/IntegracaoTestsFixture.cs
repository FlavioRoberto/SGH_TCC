using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SGH.APi;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Shared.Extensions;
using System;
using System.Collections.Generic;
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

    public class IntegracaoTestsFixture<TStartup>  where TStartup : class
    {
        public readonly AppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegracaoTestsFixture()
        {
            Factory = new AppFactory<TStartup>();
            Client = GerarClient();
        }

        internal async Task<T> RecuperarConteudoRequisicao<T>(HttpResponseMessage response)
        {
            var dados = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dados);
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

        internal async Task TestarRequisicaoComErro(HttpResponseMessage resposta, string mensagemErroEsperada)
        {
            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErroRequest = await resposta.Content.ReadAsStringAsync();

            mensagemErroRequest.Trim().Should().Be(mensagemErroEsperada.Trim());
        }

        internal async Task TestarRequisicaoComErro(HttpResponseMessage resposta, List<string> erros)
        {
            var mensagemErroEsperada = "";

            erros.ForEach(erro =>
            {
                mensagemErroEsperada += $"{erro}{Environment.NewLine}";
            });

            await TestarRequisicaoComErro(resposta, mensagemErroEsperada);
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
