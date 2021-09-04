using FluentAssertions;
using Moq;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Servicos;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using SGH.Dominio.Core.Services;
using System.Collections.Generic;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class CargoServiceTeste
    {
        private ICargoService _cargoService;
        private readonly Mock<ICargoRepositorio> _mockRepositorio;

        public CargoServiceTeste()
        {
            _mockRepositorio = new Mock<ICargoRepositorio>();
            _cargoService = new CargoService(_mockRepositorio.Object);
        }

        [Fact(DisplayName = "Retornar Professor - Deve retornar nome cargo não encontrado")]
        [Trait("Unitários", "Cargo Service")]
        public async Task CargoService_RetornarProfessor_DeveRetornarNomeCargoNaoEncontrado()
        {
            Cargo cargoRetornado = null;
            
            _mockRepositorio.Setup(lnq => lnq.Consultar(It.IsAny<Expression<Func<Cargo, bool>>>())).Returns(Task.FromResult(cargoRetornado));

            var aula = new Aula();

            aula.Disciplina = new CargoDisciplina { CodigoCargo = 1 };

            var nomeProfessor = await _cargoService.RetornarProfessor(aula);

            nomeProfessor.Should().Be("Cargo não encontrado");
        }

        [Fact(DisplayName = "Retornar Professor - Deve retornar nome de cargo para professor não encontrado")]
        [Trait("Unitários", "Cargo Service")]
        public async Task CargoService_RetornarProfessor_DeveRetornarNomeDeCargoParaProfessorNaoEncontrado()
        {
            Cargo cargoRetornado = new Cargo { Numero = 20 };

            _mockRepositorio.Setup(lnq => lnq.Listar(It.IsAny<List<long>>())).Returns(Task.FromResult(new List<Cargo> { cargoRetornado }));
            _mockRepositorio.Setup(lnq => lnq.ConsultarProfessor(It.IsAny<List<long>>())).Returns(Task.FromResult(new List<Professor>()));

            var aula = new Aula();

            aula.Disciplina = new CargoDisciplina { CodigoCargo = 1 };

            var nomeProfessor = await _cargoService.RetornarProfessor(aula);

            nomeProfessor.Should().Be($"Cargo {cargoRetornado.Numero}");
        }

        [Fact(DisplayName = "Retornar Professor - Deve retornar nome do professor vinculado ao cargo")]
        [Trait("Unitários", "Cargo Service")]
        public async Task CargoService_RetornarProfessor_DeveRetornarNomeProfessorVinculado()
        {
            var cargoRetornado = new Cargo();
            var professorRetornado = new Professor { Codigo = 1, Nome = "Professor Teste" };

            cargoRetornado.Professor = professorRetornado;

            _mockRepositorio.Setup(lnq => lnq.Listar(It.IsAny<List<long>>())).Returns(Task.FromResult(new List<Cargo> { cargoRetornado }));

            var aula = new Aula();

            aula.Disciplina = new CargoDisciplina { CodigoCargo = 1 };

            var nomeProfessor = await _cargoService.RetornarProfessor(aula);

            nomeProfessor.Should().Be(professorRetornado.Nome);
        }
    }

}
