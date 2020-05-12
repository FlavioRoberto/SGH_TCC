using FluentAssertions;
using SGH.APi;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar;
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
    public class HorarioAulaTeste
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public HorarioAulaTeste(IntegracaoTestsFixture<StartupTests> testsFixture)
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

            conteudo.Should().HaveCountGreaterOrEqualTo(4);
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

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Remover horário - Deve retornar mensagem código não inforado")]
        public async Task HorarioAula_Remover_DeveRetornarMensagemCodigoNaoInformado()
        {
            var codigoHorario = 0;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoHorario}"));

            await _testsFixture.TestarRequisicaoComErro(resposta, "O código do horário não foi informado.");
        }


        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Remover horário - Deve retornar mensagem horário não existe")]
        public async Task HorarioAula_Remover_DeveRetornarMensagemHorarioNaoExiste()
        {
            var codigoHorario = 99;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoHorario}"));

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um horário com o código {codigoHorario}.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Remover horário - Deve remover horário com sucesso")]
        public async Task HorarioAula_Remover_DeveRemoverHorarioComSucesso()
        {
            var codigoHorario = 1;

            var resposta = await _testsFixture.Client.DeleteAsync(GetRota($"remover?codigo={codigoHorario}"));

            resposta.EnsureSuccessStatusCode();
            
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem código não inforado")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCodigoNaoInformado()
        {
            var comando = new AtualizarHorarioAulaComando {
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo códgo não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem código não encontrado")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCodigoNaoEncontrado()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 99,
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um horário com o código {comando.Codigo}.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo ano não pode ser vazio")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCampoAnoNaoPodeSerVazio()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo ano não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo período não pode ser vazio")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCampoPeriodoNaoPodeSerVazio()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo período não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo semestre não pode ser vazio")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCampoSemestreNaoPodeSerVazio()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                CodigoCurriculo = 1,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo semestre não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo código do currículo não pode ser vazio")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCampoCodigoCurriculoeNaoPodeSerVazio()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo código do currículo não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo currículo não encontrado")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCurriculoNaoEncontrado()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                CodigoTurno = 1,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 99
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um currículo com o código {comando.CodigoCurriculo}.");
        }


        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo código do turno não pode ser vazio")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemCampoCodigoTurnoNaoPodeSerVazio()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, "O campo código do turno não pode ser vazio.");
        }

        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve retornar mensagem campo turno não encontrado")]
        public async Task HorarioAula_Atualizar_DeveRetornarMensagemTurnoNaoEncontrado()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2020,
                CodigoTurno = 99,
                Periodo = EPeriodo.DECIMO,
                Semestre = ESemestre.PRIMEIRO,
                CodigoCurriculo = 1
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            await _testsFixture.TestarRequisicaoComErro(resposta, $"Não foi encontrado um turno com o código {comando.CodigoTurno}.");
        }


        [Trait("Integração", "Horário de aula")]
        [Fact(DisplayName = "Atualizar - Deve atualizar horário com sucesso")]
        public async Task HorarioAula_Atualizar_DeveAtualizarHorarioComSucesso()
        {
            var comando = new AtualizarHorarioAulaComando
            {
                Codigo = 2,
                Ano = 2021,
                CodigoTurno = 2,
                Periodo = EPeriodo.NONO,
                Semestre = ESemestre.SEGUNDO,
                CodigoCurriculo = 2
            };

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("editar"), comando);

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<HorarioAulaViewModel>(resposta);

            conteudo.Should().NotBeNull();

            conteudo.Codigo.Should().Be(comando.Codigo);

            conteudo.Ano.Should().Be(comando.Ano);

            conteudo.CodigoTurno.Should().Be(comando.CodigoTurno);

            conteudo.Periodo.Should().Be(comando.Periodo);

            conteudo.Semestre.Should().Be(comando.Semestre);

            conteudo.CodigoCurriculo.Should().Be(comando.CodigoCurriculo);
        }

        [Trait("Integração", "Horário de aula")]
        [InlineData(2, 1)]
        [InlineData(4, 2)]
        [Theory(DisplayName = "Listar - Deve retornar aulas de um horário")]
        public async Task Aula_RealizarCadastro_DeveRetornarAulaDeHorario(int codigoHorario, int quantidade)
        {
            var resposta = await _testsFixture.Client.GetAsync(GetRota($"{codigoHorario}/listar-aulas"));

            resposta.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<ICollection<AulaViewModel>>(resposta);

            conteudo.Should().NotBeEmpty();

            conteudo.Should().HaveCount(quantidade);

            conteudo.Should().NotContain(lnq => lnq.CodigoHorario != codigoHorario);

        }

        private string GetRota(string rota = "")
        {
            return $"api/horario-aula/{rota}";
        }
    }
}
