using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Horarios.Consultas.Listar;
using SGH.Dominio.Services.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class HorarioAulaTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public HorarioAulaTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo ano não pode ser vazio")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCampoAnoNaoPodeSerVazio()
        {
            var comando = new CriarHorarioAulaComando { 
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo ano não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo período não pode ser vazio")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCampoPeriodoNaoPodeSerVazio()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo período não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo semestre não pode ser vazio")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCampoSemestreNaoPodeSerVazio()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo semestre não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo código do currículo não pode ser vazio")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCampoCodigoCurriculoeNaoPodeSerVazio()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo código do currículo não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo currículo não encontrado")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCurriculoNaoEncontrado()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 99
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um currículo com o código {comando.CodigoCurriculo}.");
        }


        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo código do turno não pode ser vazio")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemCampoCodigoTurnoNaoPodeSerVazio()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo código do turno não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem campo turno não encontrado")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarMensagemTurnoNaoEncontrado()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoTurno = 99,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um turno com o código {comando.CodigoTurno}.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Cadastro - Deve realizar cadastro de bloco com sucesso")]
        public async Task HorarioAula_RealizarCadastro_DeveRealizarCadastroComSucesso()
        {
            var comando = new CriarHorarioAulaComando
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Semestre = ESemestre.SEGUNDO,
                Periodo = EPeriodo.SEGUNDO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            resposta.EnsureSuccessStatusCode();

            var dadosResposta = await _testsFixture.RecuperarConteudoRequisicao<HorarioAulaViewModel>(resposta);

            dadosResposta.Codigo.Should().BeGreaterThan(0);

            dadosResposta.Ano.Should().Be(comando.Ano);

            dadosResposta.CodigoCurriculo.Should().Be(comando.CodigoCurriculo);

            dadosResposta.CodigoTurno.Should().Be(comando.CodigoTurno);

            dadosResposta.Semestre.Should().Be(comando.Semestre);

            dadosResposta.Periodo.Should().Be(comando.Periodo);
        }


        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "ListarHorarios - Deve retornar todos horarios")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarTodosHorarios()
        {
            var consulta = new ListarHorarioAulaConsulta();

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listar"), consulta);

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<List<HorarioAulaViewModel>>(resposta);

            conteudo.Should().NotBeEmpty();

            conteudo.Should().HaveCount(3);
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "ListarHorarios - Deve retornar horario filtrado")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarHorarioFiltrado()
        {
            var consulta = new ListarHorarioAulaConsulta
            {
                Ano = 2020,
                CodigoCurriculo = 1,
                Periodo = EPeriodo.PRIMEIRO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listar"), consulta);

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<List<HorarioAulaViewModel>>(resposta);

            conteudo.Should().NotBeEmpty();

            conteudo.Should().HaveCount(1);

            conteudo.Should().NotContain(lnq => lnq.Ano != consulta.Ano);

            conteudo.Should().NotContain(lnq => lnq.CodigoCurriculo != consulta.CodigoCurriculo);

            conteudo.Should().NotContain(lnq => lnq.Periodo != consulta.Periodo);

            conteudo.Should().NotContain(lnq => lnq.Semestre != consulta.Semestre);
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "ListarHorarios - Deve retornar horarios vazios")]
        public async Task HorarioAula_RealizarCadastro_DeveRetornarHorariosVazios()
        {
            var consulta = new ListarHorarioAulaConsulta
            {
                Ano = 2033
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("listar"), consulta);

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<List<HorarioAulaViewModel>>(resposta);

            conteudo.Should().BeEmpty();
        }

        private string GetRota(string rota = "")
        {
            return $"api/horario-aula/{rota}";
        }
    }
}
