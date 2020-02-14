using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using SGH.APi;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.Implementacao.Cargos.Comandos.Remover;
using SGH.Dominio.Implementacao.Cargos.Consultas.ListarPaginacao;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeIntegracao
{
    [Collection(nameof(IntegracaoWebTestsFixtureCollection))]
    public class CargoTests
    {
        private readonly IntegracaoTestsFixture<StartupTests> _testsFixture;

        public CargoTests(IntegracaoTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        #region Criar

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo sem professor com sucesso")]
        public async Task Cargo_RealizarCadastro_DeveRealizarCadastroSemProfessorComSucesso()
        {
            var comando = GerarComandoCargo();

            var cargo = await RealizarRequisicaoCargo<CargoViewModel, CriarCargoComando>("criar", HttpMethod.Post, comando);

            ValidarCargo(cargo);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo com professor com sucesso")]
        public async Task Cargo_RealizarCadastro_DeveRealizarCadastroComProfessorComSucesso()
        {
            var comando = GerarComandoCargo();

            comando.CodigoProfessor = 1;

            comando.Numero = 2;

            var cargo = await RealizarRequisicaoCargo<CargoViewModel, CriarCargoComando>("criar", HttpMethod.Post, comando);

            cargo.CodigoProfessor.Should().Be(1);

            ValidarCargo(cargo);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo inválido")]
        public async Task Cargo_RealizarCadastro_DeveRetornarMensagemCargoInvalido()
        {

            var comando = new CriarCargoComando();
            comando.CodigoProfessor = 999;

            var response = await _testsFixture.Client.PostAsync(GetRota("criar"), _testsFixture.GerarCorpoRequisicao(comando));

            response.IsSuccessStatusCode.Should().Be(false);

            var mensagemErro = await response.Content.ReadAsStringAsync();

            var mensagemEsperada = $@"O campo ano é obrigatório.
                                     O campo edital é obrigatório.
                                     O campo número é obrigatório.
                                     O campo semestre é obrigatório.
                                     Não foi encontrado um professor com o código {comando.CodigoProfessor}."
                                   .RemoverEspacosVazios();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemEsperada);

        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo igual")]
        public async Task Cargo_RealizarCadastro_DeveRetornarMensagemCargoIgual()
        {
            var comando = GerarComandoCargo();
            comando.Edital = 1;

            var response = await _testsFixture.Client.PostAsync(GetRota("criar"), _testsFixture.GerarCorpoRequisicao(comando));

            var mensagemErro = await response.Content.ReadAsStringAsync();

            var mensagemEsperada = $@"Já existe um cargo com os mesmos valores para os campos semestre, ano, edital e número."
                                   .RemoverEspacosVazios();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemEsperada);
        }

        #endregion

        #region Remover
        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar remoção de cargo")]
        public async Task Cargo_RealizarExclusão_DeveRemoverCargoComSucesso()
        {
            int codigoCargo = 1;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{codigoCargo}"));

            response.EnsureSuccessStatusCode();

            var conteudo = await _testsFixture.RecuperarConteudoRequisicao<bool>(response);

            conteudo.Should().BeTrue();
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar remoção de cargo inexistente")]
        public async Task Cargo_RealizarExclusão_DeveRemoverCargoInexistente()
        {
            int codigoCargo = 99;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{codigoCargo}"));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErro = await response.Content.ReadAsStringAsync();

            var mensagemComparar = $"Não foi encontrado um cargo com o código {codigoCargo}.".RemoverEspacosVazios();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemComparar);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar remoção de cargo sem código informado")]
        public async Task Cargo_RealizarExclusão_DeveRemoverCargoSemCodigoInformado()
        {
            int codigoCargo = 0;

            var response = await _testsFixture.Client.DeleteAsync(GetRota($"remover/{codigoCargo}"));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var mensagemErro = await response.Content.ReadAsStringAsync();

            var mensagemComparar = $"O parâmetro código é obrigatório.".RemoverEspacosVazios();

            mensagemErro.RemoverEspacosVazios().Should().Be(mensagemComparar);
        }
        #endregion

        #region Consulta
        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar consulta paginada de cargo")]
        public async Task Cargo_RealizarExclusão_DeveConsultarPaginadoComSucesso()
        {
            var consulta = new Paginacao<CargoViewModel>
            {
                Posicao = 0,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<CargoViewModel>()
            };

            var response = await RealizarRequisicaoCargo<Paginacao<CargoViewModel>, Paginacao<CargoViewModel>>("listarPaginacao", HttpMethod.Post, consulta);

            response.Quantidade.Should().Be(1);

            response.Posicao.Should().Be(1);

            response.Entidade.Should().NotBeNull();

            response.Entidade.Should().HaveCount(1);

            response.Entidade.Should().NotContain(lnq => lnq.Codigo <= 0);

            response.Entidade.Should().Contain(lnq => lnq.Codigo == 1);

        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar consulta paginada de cargo sem dados encontrados")]
        public async Task Cargo_RealizarExclusão_DeveConsultarPaginadoSemDadosEncontrados()
        {
            var consulta = new Paginacao<CargoViewModel>
            {
                Posicao = 1,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<CargoViewModel> { 
                    new CargoViewModel
                    {
                        Codigo = 99
                    }
                }
            };

            var response = await RealizarRequisicaoCargo<Paginacao<Cargo>, Paginacao<CargoViewModel>>("listarPaginacao", HttpMethod.Post, consulta);

            response.Quantidade.Should().Be(0);

            response.Posicao.Should().Be(0);

            response.Entidade.Should().NotBeNull();

            response.Entidade.Should().HaveCount(0);

        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar consulta paginada de cargo com filtros")]
        public async Task Cargo_RealizarExclusão_DeveConsultarPaginadoComFiltros()
        {
            var consulta = new Paginacao<CargoViewModel>
            {
                Posicao = 1,
                Quantidade = 1,
                Total = 0,
                Entidade = new List<CargoViewModel> {
                    new CargoViewModel
                    {
                        Codigo = 2,
                        Ano = 2020,
                        CodigoProfessor = 2,
                        Edital = 2,
                        Numero = 2,
                        Semestre = ESemestre.SEGUNDO
                    }
                }
            };

            var response = await RealizarRequisicaoCargo<Paginacao<Cargo>, Paginacao<CargoViewModel>>("listarPaginacao", HttpMethod.Post, consulta);

            response.Quantidade.Should().Be(1);

            response.Posicao.Should().Be(1);

            response.Entidade.Should().NotBeNull();

            response.Entidade.Should().HaveCount(1);

            response.Entidade.Should().NotContain(lnq => lnq.Codigo <= 0);

            response.Entidade.Should().Contain(lnq => lnq.Codigo == 2);

            response.Entidade.Should().Contain(lnq => lnq.CodigoProfessor == 2);

            response.Entidade.Should().Contain(lnq => lnq.Edital == 2);
            
            response.Entidade.Should().Contain(lnq => lnq.Numero == 2);

            response.Entidade.Should().Contain(lnq => lnq.Semestre == ESemestre.SEGUNDO);

            response.Entidade.Should().Contain(lnq => lnq.Ano == 2020);

            response.Entidade.Should().Contain(lnq => lnq.Disciplinas.Count() == 2);

            var disciplinas = response.Entidade.FirstOrDefault().Disciplinas;
            
            disciplinas.Should().NotContain(lnq => lnq.CodigoCargo != 2);

            disciplinas.Should().NotContain(lnq => lnq.Codigo <= 0);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar atualização de cargo inválido")]
        public async Task Cargo_RealizarAtualizacaoCargoInvalido_DeveRetornarMensagensDeErro()
        {
            var comando = new AtualizarCargoComando();
            comando.CodigoProfessor = 99;
            comando.Codigo = 99;

            var mensagemEsperada = $@"O campo ano é obrigatório.
                                     O campo edital é obrigatório.
                                     O campo número é obrigatório.
                                     O campo semestre é obrigatório.
                                     Não foi encontrado um professor com o código {comando.Codigo}.
                                     Não foi encontrado um cargo com o código {comando.Codigo}."
                                    .RemoverEspacosVazios();

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            var mensagemErroResposta = await resposta.Content.ReadAsStringAsync();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            mensagemErroResposta.RemoverEspacosVazios().Should().Be(mensagemEsperada);

        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar atualização de cargo ja existente")]
        public async Task Cargo_RealizarAtualizacaoCargoJaExistente_DeveRetornarMensagensDeErro()
        {
            var comando = new AtualizarCargoComando
            {
                Codigo = 2,
                Ano = DateTime.Now.Year,
                CodigoProfessor = 1,
                Edital = 1,
                Numero = 1,
                Semestre = ESemestre.PRIMEIRO
            };

            var mensagemEsperada = $@"Já existe um cargo com os mesmos valores para os campos semestre, ano, edital e número."
                                    .RemoverEspacosVazios();

            var resposta = await _testsFixture.Client.PutAsJsonAsync(GetRota("atualizar"), comando);

            var mensagemErroResposta = await resposta.Content.ReadAsStringAsync();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            mensagemErroResposta.RemoverEspacosVazios().Should().Be(mensagemEsperada);

        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar atualização de cargo sem professor com sucesso")]
        public async Task Cargo_RealizarAtualizacao_DeveRealizarAtualizacaoSemProfessorComSucesso()
        {
            var comando = new AtualizarCargoComando
            {
                Ano = 2020,
                Codigo = 3,
                Edital = 99,
                Numero = 99,
                Semestre = ESemestre.SEGUNDO
            };

            var cargo = await RealizarRequisicaoCargo<CargoViewModel, AtualizarCargoComando>("atualizar", HttpMethod.Put, comando);

            ValidarCargo(cargo);

            cargo.Ano.Should().Be(comando.Ano);
            cargo.Codigo.Should().Be(comando.Codigo);
            cargo.Edital.Should().Be(comando.Edital);
            cargo.Numero.Should().Be(comando.Numero);
            cargo.Semestre.Should().Be(comando.Semestre);
            cargo.CodigoProfessor.Should().BeNull();
        }

        #endregion

        private void ValidarCargo(CargoViewModel cargo)
        {
            cargo.Should().NotBeNull();

            cargo.Codigo.Should().BeGreaterThan(0);

            cargo.Ano.Should().BeGreaterThan(0);

            ((int)cargo.Semestre).Should().BeGreaterThan(0);

            cargo.Numero.Should().BeGreaterThan(0);

            cargo.Edital.Should().BeGreaterThan(0);

        }

        private async Task<TResposta> RealizarRequisicaoCargo<TResposta, TParametro>(string rota, HttpMethod metodoHttp, TParametro comando)
        {
            var request = new HttpRequestMessage(metodoHttp, GetRota(rota))
            {
                Content = _testsFixture.GerarCorpoRequisicao(comando)
            };

            var response = await _testsFixture.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await _testsFixture.RecuperarConteudoRequisicao<TResposta>(response);
        }

        private CriarCargoComando GerarComandoCargo()
        {
            return new CriarCargoComando
            {
                Ano = DateTime.Now.Year,
                Edital = 2,
                Numero = 1,
                Semestre = ESemestre.PRIMEIRO,
            };
        }

        private string GetRota(string rota)
        {
            return $"api/cargo/{rota}";
        }

    }
}
