using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CargoDisciplinaTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CargoDisciplinaTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo sem professor com sucesso")]
        public async Task DisciplinaCargo_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarCargoDisciplinaComando();

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<CargoDisciplinaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);           

        }

        private string GetRota(string rota = "")
        {
            return $"api/cargo/disciplinas/{rota}";
        }
    }
}
