using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

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
        [Fact(DisplayName = "Realizar cadastro de bloco sem mensagem de descrição")]
        public async Task Bloco_RealizarCadastro_DeveRetornarMensagemDescricaoObrigatoria()
        {
            var comando = new CriarBlocoComando();

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = "O campo descricão não pode estar vazio.".RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar edição de bloco com sucesso")]
        public async Task Bloco_RealizarEdicao_DeveRealizarCadastroComSucesso()
        {
            var comando = new AtualizarBlocoComando
            {
                Codigo = 1,
                Descricao = "Bloco 2"
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<BlocoViewModel>(resposta);

            dadosResposta.Codigo.Should().Be(comando.Codigo);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar edição de bloco sem mensagem de descrição")]
        public async Task Bloco_RealizarEdicao_DeveRetornarMensagemDescricaoObrigatoria()
        {
            var comando = new AtualizarBlocoComando();

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            var mensagemEsperada = @"O campo descricão não pode estar vazio.
                                     O campo código não foi informado.".RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar edição de bloco inexistente")]
        public async Task Bloco_RealizarEdicao_DeveRetornarMensagemBlocoInexistente()
        {
            var comando = new AtualizarBlocoComando
            {
                Codigo = 99,
                Descricao = "Teste edição sem código"
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            var mensagemEsperada = $@"Não foi encontrado um bloco com o código {comando.Codigo}."
                                   .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar remoção de bloco com sucesso")]
        public async Task Bloco_RealizarRemocao_DeveRemoverComSucesso()
        {
            var comando = new RemoverBlocoComando
            {
                Codigo = 2
            };

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{comando.Codigo}"));

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<bool>(resposta);

            conteudo.Should().BeTrue();

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar remoção de bloco com código não informado")]
        public async Task Bloco_RealizarRemocao_DeveRetornarMensagemCodigoNaoInformado()
        {
            var comando = new RemoverBlocoComando();

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{comando.Codigo}"));

            var mensagemEsperada = "O código do bloco não foi informado.".RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar remoção de bloco inexistente")]
        public async Task Bloco_RealizarRemocao_DeveRetornarMensagemBlocoInexistente()
        {
            var comando = new RemoverBlocoComando
            {
                Codigo = 99
            };

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{comando.Codigo}"));

            var mensagemEsperada = $"Não foi encontrado um bloco com o código {comando.Codigo}.".RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }


        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar remoção de bloco vinculado a salas")]
        public async Task Bloco_RealizarRemocao_DeveRetornarMensagemBlocoVinculadoASalas()
        {
            var comando = new RemoverBlocoComando
            {
                Codigo = 3
            };

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{comando.Codigo}"));

            var mensagemEsperada = $"Não foi possível remover o bloco de código {comando.Codigo}, pois ele está vinculado em salas."
                                  .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar consulta paginada de bloco")]
        public async Task Bloco_RealizarConsultaPaginada_DeveConsultarPaginadoComSucesso()
        {
            var consulta = new Paginacao<BlocoViewModel>
            {
                Posicao = 0,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<BlocoViewModel>()
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listarPaginacao"), consulta);

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<Paginacao<BlocoViewModel>>(resposta);

            conteudo.Quantidade.Should().Be(1);

            conteudo.Posicao.Should().Be(1);

            conteudo.Total.Should().Be(3);

            conteudo.Entidade.Should().NotBeNull();

            conteudo.Entidade.Should().HaveCount(1);

            conteudo.Entidade.Should().NotContain(lnq => lnq.Codigo <= 0);

            conteudo.Entidade.Should().Contain(lnq => lnq.Codigo == 1);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar consulta paginada de bloco sem dados encontrados")]
        public async Task Bloco_RealizarConsultaPaginada_DeveConsultarPaginadoSemDadosEncontrados()
        {
            var consulta = new Paginacao<BlocoViewModel>
            {
                Posicao = 1,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<BlocoViewModel> {
                    new BlocoViewModel
                    {
                        Codigo = 99
                    }
                }
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listarPaginacao"), consulta);

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<Paginacao<BlocoViewModel>>(resposta);

            conteudo.Quantidade.Should().Be(0);

            conteudo.Posicao.Should().Be(0);

            conteudo.Entidade.Should().NotBeNull();

            conteudo.Entidade.Should().HaveCount(0);

        }

        [Trait("Integração", "Bloco")]
        [Fact(DisplayName = "Realizar consulta paginada de bloco com filtros")]
        public async Task Bloco_RealizarConsultaPaginada__DeveConsultarPaginadoComFiltros()
        {
            var consulta = new Paginacao<BlocoViewModel>
            {
                Posicao = 1,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<BlocoViewModel> {
                    new BlocoViewModel
                    {
                        Codigo = 4,
                        Descricao = "Bloco paginado"
                    }
                }
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listarPaginacao"), consulta);

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<Paginacao<BlocoViewModel>>(resposta);

            conteudo.Quantidade.Should().Be(1);

            conteudo.Posicao.Should().Be(1);

            conteudo.Entidade.Should().NotBeNull();

            conteudo.Entidade.Should().HaveCount(1);

            conteudo.Entidade.Should().NotContain(lnq => lnq.Codigo <= 0);

            conteudo.Entidade.Should().Contain(lnq => lnq.Codigo == 4);

            conteudo.Entidade.Should().Contain(lnq => lnq.Descricao.Equals("Bloco paginado"));

        }


        private string GetRota(string rota = "")
        {
            return $"api/bloco/{rota}";
        }

    }
}
