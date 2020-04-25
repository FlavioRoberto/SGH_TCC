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
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo;
using SGH.Dominio.Core.Enums;
using System.Collections.Generic;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CargoDisciplinaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CargoDisciplinaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
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
                CodigoTurno = 1,
                Descricao = "Descrição disciplina"
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<CargoDisciplinaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);

            dadosResposta.CodigoCargo.Should().Be(comando.CodigoCargo);

            dadosResposta.CodigoCurriculoDisciplina.Should().Be(comando.CodigoCurriculoDisciplina);

            dadosResposta.Descricao.Should().Be(comando.Descricao);

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
                CodigoTurno = 2
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

            conteudo.Should().Contain(lnq => lnq.CursoDescricao.Equals($"Engenharia da computação - {anoAtual}") && 
                                      lnq.Descricao.Equals("Substituindo nome disciplina no cargo") &&
                                      lnq.TurnoDescricao.Equals("Vespertino"));

            conteudo.Should().Contain(lnq => lnq.CursoDescricao.Equals($"Engenharia civil - {anoAtual + 1}") &&
                                      lnq.Descricao.Equals("Concreto armado") &&
                                      lnq.TurnoDescricao.Equals("Noturno"));

            conteudo.Should().Contain(lnq => lnq.CursoDescricao.Equals($"Engenharia de produção - {anoAtual + 2}") && 
                                      lnq.Descricao.Equals("Cálculo I") &&
                                      lnq.TurnoDescricao.Equals("Matutino"));

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


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem ano não informado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemAnoNaoInformado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta { 
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"O campo ano não foi informado.");          
        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem periodo não informado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemPeriodoNaoInformado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"O campo período não foi informado.");
        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem semestre não informado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemSemestreNaoInformado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"O campo semestre não foi informado.");
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem turno não informado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemTurnoNaoInformado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                Periodo = EPeriodo.PRIMEIRO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"O campo código do turno não foi informado.");
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem curriculo não informado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemCurriculoNaoInformado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.PRIMEIRO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"O campo código do currículo não foi informado.");
        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem curriculo não encontrado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemCurriculoNaoEncontrado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.PRIMEIRO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 99
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um currículo com o código {consulta.CodigoCurriculo}.");
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar mensagem turno não encontrado ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarMensagemTurnoNaoEncontrado()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoTurno = 99,
                Periodo = EPeriodo.PRIMEIRO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um turno com o código {consulta.CodigoTurno}.");
        }


        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar lista vazia ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarListaVazia()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            resposta.EnsureSuccessStatusCode();

            var dados = await _testsFixture.RecuperarConteudoRequisicao<List<CargoDisciplinaViewModel>>(resposta);

            dados.Should().BeEmpty();
        }

        [Trait("Integração", "Disciplina Cargo")]
        [Fact(DisplayName = "Listar por curriculo - Deve retornar lista vazia ")]
        public async Task DisciplinaCargo_ListarDisciplinasPorCurriculo_DeveRetornarDuasDisciplinas()
        {
            var consulta = new ListarDisciplinaCargoPorCurriculoConsulta
            {
                Ano = 2020,
                CodigoTurno = 2,
                Periodo = EPeriodo.NONO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota($"listar-por-curriculo"), consulta);

            resposta.EnsureSuccessStatusCode();

            var dados = await _testsFixture.RecuperarConteudoRequisicao<List<CargoDisciplinaViewModel>>(resposta);

            dados.Should().HaveCount(2);

            dados.Should().NotContain(lnq => lnq.CursoDescricao != "Engenharia da computação" &&
                                             (lnq.Descricao != "Programação orientada a objetos" ||
                                             lnq.Descricao != "Engenharia de software"));

        }


        private string GetRota(string rota = "")
        {
            return $"api/cargo/disciplinas/{rota}";
        }
    }
}
