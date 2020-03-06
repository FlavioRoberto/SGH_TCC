﻿using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using SGH.Dominio.Shared.Extensions;


namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CurriculoDisciplinaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CurriculoDisciplinaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina do currículo com sucesso")]
        public async Task DisciplinaCurriculo_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarCurriculoDisciplinaComando
            {
                AulasSemanaisPratica = 3,
                AulasSemanaisTeorica = 2,
                CodigoCurriculo = 1,
                CodigoDisciplina = 1,
                Periodo = (int) EPeriodo.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<CurriculoDisciplinaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);
            
        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina inválida")]
        public async Task DisciplinaCurriculo_RealizarCadastro_DeveRetornarMensagemDeErroCamposObrigatorios()
        {
            var comando = new CriarCurriculoDisciplinaComando();

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"O campo período é obrigatório.
                                        O campo código da disciplina é obrigatório.
                                        O campo código do currículo é obrigatório.
                                        O campo aulas semanais teóricas é obrigatório.
                                        O campo aulas semanais práticas é obrigatório."
                                       .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemErroEsperada);
        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar cadastro de disciplina com disciplina e curriculo inválido")]
        public async Task DisciplinaCurriculo_RealizarCadastro_DeveRetornarMensagemDeErroDisciplinaECurriculoNaoEncontrado()
        {
            var comando = new CriarCurriculoDisciplinaComando
            {
                AulasSemanaisPratica = 4,
                AulasSemanaisTeorica = 4,
                CodigoCurriculo = 99,
                CodigoDisciplina = 99,
                Periodo = (int)EPeriodo.NONO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota(), comando);

            var mensagemErroEsperada = $@"Não foi encontrado uma disciplina com o código {comando.CodigoDisciplina}.
                                       Não foi encontrado um currículo com o código {comando.CodigoCurriculo}."
                                       .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemErroEsperada);
        }

        private string GetRota(string rota = "")
        {
            return $"api/curriculo/disciplinas/{rota}";
        }
    }
}
