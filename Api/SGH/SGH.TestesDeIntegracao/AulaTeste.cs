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
                "O código da sala não pode ser vazio.",
                "O código do horário não pode ser vazio.",
                "O código da disciplina não pode ser vazio.",
                "O Dia da semana não pode ser vazio.",
                "O campo hora não pode ser vazio."
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
        [Fact(DisplayName = "Cadastro - Deve retornar mensagem sala não encontrada")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemSalaNaoEncontrada()
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Terça", "18:00"),
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
                Reserva = new Reserva("quinta", "08:00"),
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
                Reserva = new Reserva("quinta", "19:20"),
                Desdobramento = false,
                CodigoDisciplina = 99,
                CodigoHorario = 1,
                CodigoSala = 1,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi encontrada uma disciplina de cargo com o código {comando.CodigoDisciplina}.",
                $"Não foi possível criar a aula, pois o cargo selecionado já está reservado para {comando.Reserva.DiaSemana} às {comando.Reserva.Hora}h."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Theory(DisplayName = "Cadastro - Deve retornar mensagem horário não disponível para esse dia e horario")]
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
                $"Não foi possível criar a aula para {comando.Reserva.DiaSemana} às {comando.Reserva.Hora}h, pois já tem uma aula reservada para esse dia e horário."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Theory(DisplayName = "Cadastro - Deve retornar mensagem horário não disponível para essa sala")]
        [InlineData(1, 2, 2)]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemHorarioNaoDisponivelParaEssaSala(int codigoSala, int codigoDisciplina, int codigoHorario)
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Quarta", "08:00"),
                Desdobramento = false,
                CodigoDisciplina = codigoDisciplina,
                CodigoHorario = codigoHorario,
                CodigoSala = codigoSala,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi possível criar a aula, pois a sala selecionada já está reservada para {comando.Reserva.DiaSemana} às {comando.Reserva.Hora}h."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Theory(DisplayName = "Cadastro - Deve retornar mensagem horário não disponível para o cargo selecionado")]
        [InlineData(2, 3, 4)]
        [InlineData(1, 3, 4)]
        [InlineData(1, 2, 5)]
        [InlineData(2, 2, 5)]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemCargoNaoDisponivel(int codigoSala, int codigoDisciplina, int codigoHorario)
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Quarta", "09:00"),
                Desdobramento = false,
                CodigoDisciplina = codigoDisciplina,
                CodigoHorario = codigoHorario,
                CodigoSala = codigoSala,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi possível criar a aula, pois o cargo selecionado já está reservado para {comando.Reserva.DiaSemana} às {comando.Reserva.Hora}h."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Theory(DisplayName = "Cadastro - Deve retornar mensagem horário não disponível para professor selecionado")]
        [InlineData(4, 14, 7)]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemProfessorDisponivel(int codigoSala, int codigoDisciplina, int codigoHorario)
        {
            var comando = new CriarAulaComando
            {
                Reserva = new Reserva("Sexta", "11:00"),
                Desdobramento = false,
                CodigoDisciplina = codigoDisciplina,
                CodigoHorario = codigoHorario,
                CodigoSala = codigoSala,
                Laboratorio = false
            };

            var resposta = await _testsFixture.Client.PostAsJsonAsync(GetRota("criar"), comando);

            var mensagemEsperada = new List<string> {
                $"Não foi possível criar a aula, pois o professor selecionado já está reservado para {comando.Reserva.DiaSemana} às {comando.Reserva.Hora}h."
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

            conteudo.DescricaoDesdobramento.Should().Be(comando.DescricaoDesdobramento);

            conteudo.DescricaoDesdobramento.Should().Be(comando.DescricaoDesdobramento);

            conteudo.CodigoSala.Should().Be(comando.CodigoSala);

        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem código aula não foi informado")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemCodigoAulaNaoInformado()
        {
            var resposta = await _testsFixture.Client.DeleteAsync(GetRota("0"));

            var mensagemEsperada = new List<string> {
                "O código da aula não foi informado."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Remover - Deve retornar mensagem aula não encontrada")]
        public async Task Aula_RealizarCadastro_DeveRetornarMensagemAulaNaoEncontrada()
        {
            int codigoAula = 99;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigoAula}"));

            var mensagemEsperada = new List<string> {
                $"Não foi encontrada uma aula com o código {codigoAula}."
            };

            await _testsFixture.TestarRequisicaoComErro(resposta, mensagemEsperada);
        }

        [Trait("Integração", "Aula")]
        [Fact(DisplayName = "Remover - Deve remover aula com sucesso")]
        public async Task Aula_RealizarCadastro_DeveRemoverAulaComSucesso()
        {
            int codigoAula = 5;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"{codigoAula}"));

            resposta.EnsureSuccessStatusCode();
        }

        private string GetRota(string rota = "")
        {
            return $"api/aula/{rota}";
        }
    }
}
