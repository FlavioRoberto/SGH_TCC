using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using SGH.TestesDeIntegracao.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class AulaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public AulaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem de campos obrigatórios")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemCamposObrigatorios()
        {
            var comando = new CriarAulaComando {
                Reserva = new Reserva("", "")
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                "O Dia da semana não pode ser vazio.",
                "O campo hora não pode ser vazio.",
                "O código da sala não pode ser vazio.",
                "O código do horário não pode ser vazio.",
                "O código da disciplina não pode ser vazio."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem não foi informada descrição para desdobramento")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemDescricaoDesdobramentoNaoInformada()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça","08:00"),
                Desdobramento = true,
                CodigoDisciplina = 1,
                CodigoHorario = 1,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                "Não foi informada uma descrição para o desdobramento."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem horário reservado")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemHorarioReservado()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Quarta", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = 1,
                CodigoHorario = 1,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                "Não foi possível criar a aula nesse horário, pois já tem uma aula reservada para esse dia e horário."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem sala não encontrada")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemSalaNaoEncontrada()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = 1,
                CodigoHorario = 1,
                CodigoSala = 99,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi encontrada uma sala com o código {comando.CodigoSala}."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem horario não encontrado")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemHorarioNaoEncontrado()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = 1,
                CodigoHorario = 99,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi encontrado um horário com o código {comando.CodigoHorario}"
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem disciplina não encontrada")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemDisciplinaNaoEncontrada()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = 99,
                CodigoHorario = 1,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi encontrada uma disciplina de cargo com o código {comando.CodigoDisciplina}."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Theory(DisplayName = "Cadastro - Deve retornar mensagem horário não disponível")]
        [InlineData(1,1)]
        [InlineData(2,2)]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemHorarioNaoDisponivel(int codigoSala, int codigoDisciplina)
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Quarta", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = codigoDisciplina,
                CodigoHorario = 1,
                CodigoSala = codigoSala,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                "Não foi possível criar a aula nesse horário, pois já tem uma aula reservada para esse dia e horário."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Cadastro - Deve cadastrar aula com sucesso")]
        public async Task Aula_RealizarCadastro_DeveCadastrarAula()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça", "08:00"),
                Desdobramento = true,
                DescricaoDesdobramento = "Turma A",
                CodigoDisciplina = 1,
                CodigoHorario = 1,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<AulaViewModel>(resposta);

            conteudo.Should().NotBeNull();

            conteudo.Codigo.Should().BeGreaterThan(0);

            conteudo.Reserva.Should().BeEquivalentTo(comando.Reserva);

            conteudo.Laboratorio.Should().Be(comando.Laboratorio);

            conteudo.Desdobramento.Should().Be(comando.Desdobramento);

            conteudo.DescricaoDesdobramento.Should().Be(comando.DescricaoDesdobramento);

            conteudo.CodigoDisciplina.Should().Be(comando.CodigoDisciplina);

            conteudo.CodigoHorario.Should().Be(comando.CodigoHorario);

            conteudo.CodigoSala.Should().Be(comando.CodigoSala);

        }

        private string GetRota(string rota = "")
        {
            return $"api/aula/{rota}";
        }
    }
}
