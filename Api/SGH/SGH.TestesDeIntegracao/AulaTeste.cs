using SGH.APi;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.TestesDeIntegracao.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class AulaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public AulaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem de campos obrigatórios")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemCamposObrigatorios()
        {
            var comando = new CriarAulaComando {
                Reserva = new Reserva("", "")
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                "O Dia da semana não pode ser vazio.",
                "O campo hora não pode ser vazio.",
                "O código da sala não pode ser vazio.",
                "O código do horário não pode ser vazio.",
                "O código da disciplina não pode ser vazio."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/aula/{rota}";
        }
    }
}
