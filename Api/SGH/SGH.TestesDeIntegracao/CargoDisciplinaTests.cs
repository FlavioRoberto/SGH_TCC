using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CargoDisciplinaTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CargoDisciplinaTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina do cargo com sucesso")]
        public async Task DisciplinaCargo_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarCargoDisciplinaComando { 
               CodigoCargo = 1,
               CodigoCurriculoDisciplina = 3
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<CargoDisciplinaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);

            dadosResposta.CodigoCargo.Should().Be(comando.CodigoCargo);

            dadosResposta.CodigoCurriculoDisciplina.Should().Be(comando.CodigoCurriculoDisciplina);

        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina inválida.")]
        public async Task DisciplinaCargo_RealizarCadastro_DeveRetornarMensagemDeErroCodigoMaiorQueZero()
        {
            var comando = new CriarCargoDisciplinaComando
            {
                CodigoCargo = 0,
                CodigoCurriculoDisciplina = 0
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = @"O campo código do cargo não pode ser menor ou igual a 0.
                                         O campo código da disciplina do currículo não pode ser menor ou igual a 0."
                                        .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErroRequest = await resposta.Content.ReadAsStringAsync();

            mensagemErroRequest.RemoverEspacosVazios().Should().Be(mensagemErroEsperada);

        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina com cargo e disciplina currículo inexistentes.")]
        public async Task DisciplinaCargo_RealizarCadastro_DeveRetornarMensagemDeErroCargoDisciplinaCurriculoInexistente()
        {
            var comando = new CriarCargoDisciplinaComando
            {
                CodigoCargo = 99,
                CodigoCurriculoDisciplina = 99
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"Não foi encontrado um cargo de código {comando.CodigoCargo}.
                                          Não foi encontrado a disciplina de currículo com código {comando.CodigoCurriculoDisciplina}."
                                         .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErroRequest = await resposta.Content.ReadAsStringAsync();

            mensagemErroRequest.RemoverEspacosVazios().Should().Be(mensagemErroEsperada);

        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina ja existente.")]
        public async Task DisciplinaCargo_RealizarCadastro_DeveRetornarMensagemDeErroDisciplinaExistente()
        {
            var comando = new CriarCargoDisciplinaComando
            {
                CodigoCargo = 1,
                CodigoCurriculoDisciplina = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"Já foi adicionado uma disciplina com os mesmos valores."
                                         .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErroRequest = await resposta.Content.ReadAsStringAsync();

            mensagemErroRequest.RemoverEspacosVazios().Should().Be(mensagemErroEsperada);

        }

        private string GetRota(string rota = "")
        {
            return $"api/cargo/disciplinas/{rota}";
        }
    }
}
