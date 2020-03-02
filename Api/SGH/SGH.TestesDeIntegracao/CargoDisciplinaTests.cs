using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System;
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
            var comando = new CriarCargoDisciplinaComando
            {
                CodigoCargo = 2,
                CodigoCurriculoDisciplina = 3,
                CodigoTurno = 1
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
                CodigoCurriculoDisciplina = 0,
                CodigoTurno = 0
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = @"O campo código do cargo não pode ser menor ou igual a 0.
                                         O campo código da disciplina do currículo não pode ser menor ou igual a 0.
                                         O campo código do turno não pode ser menor ou igual a 0."
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
                CodigoCurriculoDisciplina = 99,
                CodigoTurno = 99
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"Não foi encontrado um cargo de código {comando.CodigoCargo}.
                                          Não foi encontrado a disciplina de currículo com código {comando.CodigoCurriculoDisciplina}.
                                          Não foi encontrado um turno com código {comando.CodigoTurno}."
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
                CodigoCargo = 2,
                CodigoCurriculoDisciplina = 1,
                CodigoTurno = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"Já foi adicionado uma disciplina com os mesmos valores."
                                         .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErroRequest = await resposta.Content.ReadAsStringAsync();

            mensagemErroRequest.RemoverEspacosVazios().Should().Be(mensagemErroEsperada);

        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar remoção de disciplina com sucesso.")]
        public async Task DisciplinaCargo_Remover_DeveRemoverComSucesso()
        {
            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"7"));

            resposta.EnsureSuccessStatusCode();  
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar remoção de disciplina com código igual a 0.")]
        public async Task DisciplinaCargo_Remover_DeveRetornarMensagemCodigoIgualZero()
        {
            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"0"));

            var mensagemEsperada = "O campo código não pode ser menor ou igual a 0."
                                   .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErro = await resposta.Content.ReadAsStringAsync();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemEsperada);

        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar remoção de disciplina inexistente.")]
        public async Task DisciplinaCargo_Remover_DeveRetornarMensagemDisciplinaInexistente()
        {
            int codigo = 99;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigo}"));

            var mensagemEsperada = $"Não foi encontrada uma disciplina com o código {codigo}."
                                   .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErro = await resposta.Content.ReadAsStringAsync();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemEsperada);

        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar consulta de disciplinas do cargo")]
        public async Task DisciplinaCargo_ConsultarDisciplinas_DeveRetornarDisciplinasCargoComSucesso()
        {
            int codigo = 4;

            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigo}"));

            var anoAtual = DateTime.Now.Year;

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<CargoDisciplinaViewModel[]>(resposta);

            conteudo.Should().NotContain(lnq => lnq.CodigoCargo != codigo);

            conteudo.Should().HaveCount(3);

            conteudo.Should().Contain(lnq => lnq.cursoDescricao.Equals($"Engenharia da computação - {anoAtual}") && 
                                      lnq.disciplinaDescricao.Equals("Programação para dispositivos móveis"));

            conteudo.Should().Contain(lnq => lnq.cursoDescricao.Equals($"Engenharia civil - {anoAtual + 1}") &&
                                      lnq.disciplinaDescricao.Equals("Concreto armado"));

            conteudo.Should().Contain(lnq => lnq.cursoDescricao.Equals($"Engenharia de produção - {anoAtual + 2}") && 
                                      lnq.disciplinaDescricao.Equals("Cálculo I"));
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar consulta de disciplinas do cargo com código 0")]
        public async Task DisciplinaCargo_ConsultarDisciplinas_DeveRetornarMensagemCodigoCargoZero()
        {
            int codigo = 0;

            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigo}"));

            var mensagemExperada = "O campo código não pode ter valor menor ou igual a 0."
                                   .RemoverEspacosVazios();
            
            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var erro = await resposta.Content.ReadAsStringAsync();

            erro.RemoverEspacosVazios().Should().Be(mensagemExperada);
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Realizar consulta de disciplinas do cargo inexistente")]
        public async Task DisciplinaCargo_ConsultarDisciplinas_DeveRetornarMensagemCargoInexistente()
        {
            int codigo = 99;

            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigo}"));

            var mensagemExperada = $"Não foi encontrado um cargo com código {codigo}."
                                   .RemoverEspacosVazios();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var erro = await resposta.Content.ReadAsStringAsync();

            erro.RemoverEspacosVazios().Should().Be(mensagemExperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/cargo/disciplinas/{rota}";
        }
    }
}
