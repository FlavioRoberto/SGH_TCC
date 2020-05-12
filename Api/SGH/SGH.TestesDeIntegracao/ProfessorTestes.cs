using FluentAssertions;
using SGH.APi;
using SGH.TestesDeIntegracao.Config;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class ProfessorTestes
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public ProfessorTestes(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Professor")]
        [Fact(DisplayName = "Remover - Deverá retornar mensagem professor vinculado a cargo")]
        public async Task Professor_RealizarExclusão_DeveRetornarMensagemProfessorVinculadoCargo()
        {
            int codigoProfessor = 1;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoProfessor}"));

            var mensagemComparar = $"Não foi possível remover esse professor, pois ele está vinculado ao cargo de código 9.";

            await _testsFixture.TestarRequisicaoComErro(response, mensagemComparar);
        }

        [Trait("Integração", "Professor")]
        [Fact(DisplayName = "Remover - Deverá retornar mensagem codigo professor não pode ser vazio")]
        public async Task Professor_RealizarExclusão_DeveRetornarMensagemCodigoProfessorNaoPodeSerVazio()
        {
            int codigoProfessor = 0;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoProfessor}"));

            var mensagemComparar = $"O código do professor não pode ser vazio.";

            await _testsFixture.TestarRequisicaoComErro(response, mensagemComparar);
        }

        [Trait("Integração", "Professor")]
        [Fact(DisplayName = "Remover - Deverá retornar mensagem professor não foi encontrado")]
        public async Task Professor_RealizarExclusão_DeveRetornarMensagemProfessorNaoEncontrado()
        {
            int codigoProfessor = 99;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoProfessor}"));

            var mensagemComparar = $"Não foi encontrado um professor com o código {codigoProfessor}.";

            await _testsFixture.TestarRequisicaoComErro(response, mensagemComparar);
        }

        [Trait("Integração", "Professor")]
        [Fact(DisplayName = "Remover - Deverá remover professor com sucesso")]
        public async Task Professor_RealizarExclusão_DeveRemoverProfessor()
        {
            int codigoProfessor = 4;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoProfessor}"));

            response.EnsureSuccessStatusCode();
        }

        private string GetRota(string rota)
        {
            return $"api/professor/{rota}";
        }
    }
}
