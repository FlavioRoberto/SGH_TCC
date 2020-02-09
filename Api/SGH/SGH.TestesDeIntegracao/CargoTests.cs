using FluentAssertions;
using Microsoft.AspNetCore.Http;
using SGH.APi;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.ViewModel;
using SGH.TestesDeIntegracao.Config;
using System;
using System.Collections.Generic;
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

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo sem professor com sucesso")]
        public async Task Cargo_RealizarCadastro_DeveRealizarCadastroSemProfessorComSucesso()
        {
            var comando = GerarComandoCargo();

            var cargo = await RealizarRequisicaoCargo(HttpMethod.Post, comando);

            VaidarCargo(cargo);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo com professor com sucesso")]
        public async Task Cargo_RealizarCadastro_DeveRealizarCadastroComProfessorComSucesso()
        {
            var comando = GerarComandoCargo();

            comando.CodigoProfessor = 1;

            var cargo = await RealizarRequisicaoCargo(HttpMethod.Post, comando);

            cargo.CodigoProfessor.Should().Be(1);

            VaidarCargo(cargo);
        }

        [Trait("Integração", "Cargo")]
        [Fact(DisplayName = "Realizar cadastro de cargo inválido")]
        public async Task Cargo_RealizarCadastro_DeveRetornarMensagemCargoInvalido() {


            Assert.True(false);

            //validar se campos preenchidos

            //validar se professor existente

            //validar se curriculo disciplina existente

            //validar se disciplinas maiores que 0

            //validar se cargo existente para semestre, ano, edital e numero iguais

            //Validar se disciplinas possuem codigo
        }

        private void VaidarCargo(Cargo cargo)
        {
            cargo.Should().NotBeNull();

            cargo.Codigo.Should().BeGreaterThan(0);

            cargo.Ano.Should().BeGreaterThan(0);

            ((int)cargo.Semestre).Should().BeGreaterThan(0);

            cargo.Numero.Should().BeGreaterThan(0);

            cargo.Edital.Should().BeGreaterThan(0);

            cargo.Disciplinas.Should().NotBeEmpty();

            cargo.Disciplinas.Should().NotContain(lnq => lnq.Codigo <= 0 || lnq.CodigoCargo <= 0 || lnq.CodigoCurriculoDisciplina <= 0);
            
        }

        private async Task<Cargo> RealizarRequisicaoCargo(HttpMethod metodoHttp, CriarCargoComando comando)
        {
            var request = new HttpRequestMessage(metodoHttp, GetRota("criar"))
            {
                Content = _testsFixture.GerarCorpoRequisicao(comando)
            };

            var response = await _testsFixture.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await _testsFixture.RecuperarConteudoRequisicao<Cargo>(response);
        }

        private CriarCargoComando GerarComandoCargo()
        {
            var disciplinas = new List<CargoDisciplinaViewModel>
            {
                new CargoDisciplinaViewModel {
                    CodigoCurriculoDisciplina = 1
                }
            };

            return new CriarCargoComando
            {
                Ano = DateTime.Now.Year,
                Edital = 1,
                Numero = 1,
                Semestre = ESemestre.PRIMEIRO,
                Disciplinas = disciplinas
            };
        }

        private string GetRota(string rota)
        {
            return $"api/cargo/{rota}";
        }

    }
}
