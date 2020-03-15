using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using SGH.Dominio.Shared.Extensions;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class BlocoTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public BlocoTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar cadastro de bloco com sucesso")]
        public async Task Bloco_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarBlocoComando
            {
                Descricao = "Bloco 1"
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<BlocoViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar cadastro de bloco deve retornar mensagem de descrição obrigatória")]
        public async Task Bloco_RealizarCadastro_DeveRetornarMensagemDescricaoObrigatoria()
        {
            var comando = new CriarBlocoComando();

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = "O campo descricão não pode estar vazio.".RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        private string GetRota(string rota = "")
        {
            return $"api/bloco/{rota}";
        }

    }
}
