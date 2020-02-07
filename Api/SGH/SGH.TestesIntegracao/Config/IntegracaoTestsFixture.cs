using Microsoft.AspNetCore.Mvc.Testing;
using SGH.Api;
using SGH.TestesDeIntegracao.Config;
using System;
using System.Net.Http;
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
            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            Factory = new AppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
