using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class SalaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;
               
        public SalaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
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

            var erros = new List<string>
            {
                 "O campo descrição é obrigatório.",
                 "O campo número é obrigatório.",
                 "O campo código do bloco é obrigatório.",
                 "O campo número é obrigatório."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, erros);
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

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            var erros = new List<string>
            {
                "O campo descrição é obrigatório.",
                "O campo número é obrigatório.",
                "O campo código do bloco é obrigatório.",
                "O campo número é obrigatório.",
                "O campo código é obrigatório."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, erros);
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

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

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

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

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

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<SalaViewModel>(resposta);

            dadosResposta.Codigo.Should().Be(comando.Codigo);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

            dadosResposta.Laboratorio.Should().Be(comando.Laboratorio);

            dadosResposta.Numero.Should().Be(comando.Numero);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem sala não encontrada.")]
        public async Task Sala_RealizarRemocao_DeveRetornarMensagemDeSalaNaoEncontrada()
        {
            int codigoSala = 99;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoSala}"));

            var mensagemEsperada = $@"Não foi encontrado uma sala com o código {codigoSala}.";

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Remover - Deve remover a sala com sucesso.")]
        public async Task Sala_RealizarRemocao_DeveRemoverSalaComSucesso()
        {
            int codigoSala = 3;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoSala}"));

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<bool>(resposta);

            dadosResposta.Should().BeTrue();
        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Realizar consulta paginada de sala")]
        public async Task Sala_RealizarConsultaPaginada_DeveConsultarPaginadoComSucesso()
        {
            var consulta = new Paginacao<SalaViewModel>
            {
                Posicao = 0,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<SalaViewModel>()
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listarPaginacao"), consulta);

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<Paginacao<SalaViewModel>>(resposta);

            conteudo.Quantidade.Should().Be(1);

            conteudo.Posicao.Should().Be(1);

            conteudo.Total.Should().BeGreaterOrEqualTo(2);

            conteudo.Entidade.Should().NotBeNull();

            conteudo.Entidade.Should().HaveCount(1);

            conteudo.Entidade.Should().NotContain(lnq => lnq.Codigo <= 0);

            conteudo.Entidade.Should().Contain(lnq => lnq.Codigo == 1);

        }

        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Realizar consulta paginada de sala sem dados encontrados")]
        public async Task Sala_RealizarConsultaPaginada_DeveConsultarPaginadoSemDadosEncontrados()
        {
            var consulta = new Paginacao<SalaViewModel>
            {
                Posicao = 1,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<SalaViewModel> {
                    new SalaViewModel
                    {
                        Codigo = 99
                    }
                }
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listarPaginacao"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, "Nenhuma sala encontrada.");
        }


        [Trait("Integração", "Sala")]
        [Fact(DisplayName = "Listar salas - Deverá retornar todas as salas")]
        public async Task Sala_RealizarConsultaSala_DeveConsultarTodasSalas()
        {
            var resposta = await _testsFixture.Client.GetAsync(GetRota("listarTodos"));

            resposta.EnsureSuccessStatusCode();

            var dados = await _testsFixture.RecuperarConteudoRequisicao<List<SalaViewModel>>(resposta);

            dados.Should().NotBeEmpty();

            dados.Should().HaveCount(5);
        }

        private string GetRota(string rota = "")
        {
            return $"api/sala/{rota}";
        }
    }
}
