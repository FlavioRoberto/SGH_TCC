using FluentAssertions;
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
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover;
using System.Collections.Generic;
using System.Linq;

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
                Periodo = (int)EPeriodo.PRIMEIRO
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

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar remoção de disciplina do currículo com sucesso")]
        public async Task DisciplinaCurriculo_RealizarRemocao_DeveRemoverComSucesso()
        {
            var codigoDisciplina = 7;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigoDisciplina}"));

            resposta.EnsureSuccessStatusCode();

        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar remoção de disciplina do currículo inexistente")]
        public async Task DisciplinaCurriculo_RealizarRemocao_DeveRetornarMensagemDisciplinaNaoExiste()
        {
            var codigoDisciplina = 99;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigoDisciplina}"));

            var mensagemErroEsperada = $@"Não foi encontrado uma disciplina do currículo com código {codigoDisciplina}."
                                        .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemErroEsperada);

        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar remoção de disciplina do currículo com cargo vinculado")]
        public async Task DisciplinaCurriculo_RealizarRemocao_DeveRetornarMensagemDisciplinaCargoVinculado()
        {
            var codigoDisciplina = 1;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigoDisciplina}"));

            var mensagemErroEsperada = "Não foi possível remover a disciplina pois ela está vinculada em algum cargo."
                                       .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemErroEsperada);

        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar consulta de disciplinas do currículo com código 0")]
        public async Task DisciplinaCargo_ConsultarDisciplinas_DeveRetornarMensagemCodigoCargoZero()
        {
            int codigo = 0;

            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigo}"));

            var mensagemExperada = "O campo código do currículo não pode ter valor menor ou igual a 0."
                                   .RemoverEspacosVazios();

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemExperada);
        }

        [Trait("Integração", "Disciplina currículo")]
        [Fact(DisplayName = "Realizar consulta de disciplinas do currículo")]
        public async Task DisciplinaCargo_ConsultarDisciplinas_DeveRetornarAsDisciplinasDoCurriculo()
        {
            int codigo = 4;

            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigo}"));

            resposta.EnsureSuccessStatusCode();

            var disciplinasCurriculo = await _testsFixture.RecuperarConteudoRequisicao<List<CurriculoDisciplinaViewModel>>(resposta);

            var preRequisitos = disciplinasCurriculo.FirstOrDefault().PreRequisitos;

            disciplinasCurriculo.Should().HaveCount(2);

            disciplinasCurriculo.Should().NotContain(lnq => lnq.CodigoCurriculo != codigo &&
                                 (lnq.Codigo != 9 && lnq.Codigo != 10));

            preRequisitos.Should().HaveCount(2);

            preRequisitos.Should().NotContain(lnq => lnq.Codigo != 2 && lnq.Codigo != 1);
        }

        private string GetRota(string rota = "")
        {
            return $"api/curriculo/disciplinas/{rota}";
        }
    }
}
