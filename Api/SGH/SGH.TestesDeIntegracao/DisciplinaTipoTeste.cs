using SGH.APi;
using SGH.TestesDeIntegracao.Config;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class DisciplinaTipoTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public DisciplinaTipoTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Disciplina Tipo")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem de disciplina tipo vinculada a disciplina")]
        public async Task DisciplinaTipo_RealizarRemocao_DeveRetornarMensagemDisciplinaTipoVinculadaDisciplina()
        {
            var codigoDisciplina = 1;

            var codigoTipoDisciplina = 1;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoTipoDisciplina}"));

            var mensagemEsperada = $"Não foi possível remover o tipo de disciplina pois esse tipo está vinculado a disciplina de código {codigoDisciplina}.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/disciplinaTipo/{rota}";
        }
    }
}
