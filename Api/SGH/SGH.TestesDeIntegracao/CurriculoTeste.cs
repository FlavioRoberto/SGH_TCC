using SGH.APi;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CurriculoTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CurriculoTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Currículo")]
        [Fact(DisplayName = "Remover currículo - Deverá retornar mensagem currículo vinculado a horários")]
        public async Task DisciplinaCurriculo_RemoverCurriculo_DeveRetornarMensagemCurriculoVinculadoHorario()
        {
            var codigoCurriculo = 5;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoCurriculo}"));

            var mensagemErroEsperada = "Não foi possível remover esse currículo pois ele está vinculado a horários.";                                      

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemErroEsperada);

        }

        private string GetRota(string rota = "")
        {
            return $"api/curriculo/{rota}";
        }
    }
}
