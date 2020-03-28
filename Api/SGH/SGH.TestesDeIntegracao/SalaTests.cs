using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar;
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
        public async Task Sala_RealizarCadastro_DeveRealizarCadastroComSucesso()
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
        public async Task Sala_RealizarCadastro_DeveRetornarMensagemDeCamposObrigatorios()
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
        public async Task Sala_RealizarCadastro_DeveRetornarMensagemBlocoNaoEncontrado()
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
     
        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Atualizacao - Deve retornar mensagem de campos obrigatórios")]
        public async Task Sala_RealizarAtualizacao_DeveRetornarMensagemDeCamposObrigatorios()
        {
            var comando = new AtualizarSalaComando();

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            var mensagemEsperada = @"O campo descrição é obrigatório.
                                     O campo número é obrigatório.
                                     O campo código do bloco é obrigatório.
                                     O campo número é obrigatório.
                                     O campo código é obrigatório.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Atualizacao - Deve retornar mensagem de bloco não encontrado.")]
        public async Task Sala_RealizarAtualizacao_DeveRetornarMensagemDeBlocoNaoEncontrado()
        {
            var comando = new AtualizarSalaComando
            {
                Codigo = 1,
                CodigoBloco = 99,
                Descricao = "Teste bloco inexistente",
                Laboratorio = false,
                Numero = 101
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            var mensagemEsperada = $@"Não foi encontrado um bloco com o código {comando.CodigoBloco}.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Atualizacao - Deve retornar mensagem de sala não encontrada.")]
        public async Task Sala_RealizarAtualizacao_DeveRetornarMensagemDeSalaNaoEncontrada()
        {
            var comando = new AtualizarSalaComando
            {
                Codigo = 99,
                CodigoBloco = 1,
                Descricao = "Teste sala inexistente",
                Laboratorio = false,
                Numero = 101
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            var mensagemEsperada = $@"Não foi encontrada uma sala com o código {comando.Codigo}.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Atualizacao - Deve atualizar a sala com sucesso.")]
        public async Task Sala_RealizarAtualizacao_DeveAtualizarSalaComSucesso()
        {
            var comando = new AtualizarSalaComando
            {
                Codigo = 2,
                CodigoBloco = 1,
                Descricao = "Atualizando sala",
                Laboratorio = false,
                Numero = 102
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<SalaViewModel>(resposta);

            dadosResposta.Codigo.Should().Be(comando.Codigo);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

            dadosResposta.Laboratorio.Should().Be(comando.Laboratorio);

            dadosResposta.Numero.Should().Be(comando.Numero);
        }

        private string GetRota(string rota = "")
        {
            return $"api/sala/{rota}";
        }
    }
}
