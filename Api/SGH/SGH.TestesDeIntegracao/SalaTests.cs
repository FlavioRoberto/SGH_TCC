using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class SalaTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;
               
        public SalaTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Cadastro - Deve realizar cadastro com sucesso")]
        public async Task Bloco_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarSalaComando
            {
                Descricao = "Sala 1",
                CodigoBloco = 1,
                Laboratorio = false,
                Numero = 200
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<SalaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

            dadosResposta.Laboratorio.Should().Be(comando.Laboratorio);

            dadosResposta.Numero.Should().Be(comando.Numero);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem de campos obrigatórios")]
        public async Task Bloco_RealizarCadastro_DeveRetornarMensagemDeCamposObrigatorios()
        {
            var comando = new CriarSalaComando();
        
            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = @"O campo descrição é obrigatório.
                                     O campo número é obrigatório.
                                     O campo código do bloco é obrigatório.
                                     O campo número é obrigatório.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem para bloco não encontrado")]
        public async Task Bloco_RealizarCadastro_DeveRetornarMensagemBlocoNaoEncontrado()
        {
            var comando = new CriarSalaComando
            {
                CodigoBloco = 99,
                Descricao = "Teste",
                Laboratorio = true,
                Numero = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = $@"Não foi encontrado um bloco com o código {comando.CodigoBloco}.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/sala/{rota}";
        }
    }
}
