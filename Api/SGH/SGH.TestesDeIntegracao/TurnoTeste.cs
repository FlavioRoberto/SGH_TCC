using SGH.APi;
using SGH.TestesDeIntegracao.Config;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class TurnoTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public TurnoTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Turno")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem de turno vinculado a disciplina de cargo")]
        public async Task Turno_RealizarRemocao_DeveRetornarMensagemTurnoVinculadoDisciplinaCargo()
        {
            var codigoCargo = 3;

            var codigoTurno = 1;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoTurno}"));

            var mensagemEsperada = $"Não foi possível remover o turno pois ele está vinculado a disciplina do cargo de código {codigoCargo}.";
                                 
            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Turno")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem de turno vinculado a horario")]
        public async Task Turno_RealizarRemocao_DeveRetornarMensagemTurnoVinculadoHorario()
        {
            var codigoTurno = 4;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoTurno}"));

            var mensagemEsperada = $"Não foi possível remover o turno pois ele está vinculado em horarios.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/turno/{rota}";
        }
    }
}
