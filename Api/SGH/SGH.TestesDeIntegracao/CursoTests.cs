using SGH.APi;
using SGH.TestesDeIntegracao.Config;
using SGH.Dominio.Shared.Extensions;
using Xunit;
using System.Threading.Tasks;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CursoTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CursoTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Curso")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem de curso vinculado ao currículo")]
        public async Task Curso_RealizarRemocao_DeveRetornarMensagemCursoVinculadoCurriculo()
        {
            var codigoCurso = 1;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoCurso}"));

            var mensagemEsperada = $"Não foi possível remover o curso pois ele está vinculado ao currículo de código {codigoCurso}."
                                   .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/curso/{rota}";
        }
    }
}
