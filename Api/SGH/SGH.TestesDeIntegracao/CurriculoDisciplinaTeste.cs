using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CurriculoDisciplinaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CurriculoDisciplinaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina do currículo com sucesso")]
        public async Task DisciplinaCurriculo_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarCurriculoDisciplinaComando
            {
                AulasSemanaisPratica = 3,
                AulasSemanaisTeorica = 2,
                CodigoCurriculo = 1,
                CodigoDisciplina = 1,
                Periodo = (int) EPeriodo.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<CurriculoDisciplinaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);
            

        }

        private string GetRota(string rota = "")
        {
            return $"api/curriculo/disciplinas/{rota}";
        }
    }
}
