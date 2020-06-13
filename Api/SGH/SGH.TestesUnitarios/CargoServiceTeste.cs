using FluentAssertions;
using Moq;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Servicos;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

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

            var nomeProfessor = await _cargoService.RetornarProfessor(1);

            nomeProfessor.Should().Be("Cargo não encontrado");
        }

        [Fact(DisplayName = "Retornar Professor - Deve retornar nome de cargo para professor não encontrado")]
        [Trait("Unitários", "Cargo Service")]
        public async Task CargoService_RetornarProfessor_DeveRetornarNomeDeCargoParaProfessorNaoEncontrado()
        {
            Cargo cargoRetornado = new Cargo { Numero = 20 };
            Professor professorRetornado = null;

            _mockRepositorio.Setup(lnq => lnq.Consultar(It.IsAny<Expression<Func<Cargo, bool>>>())).Returns(Task.FromResult(cargoRetornado));
            _mockRepositorio.Setup(lnq => lnq.ConsultarProfessor(It.IsAny<int>())).Returns(Task.FromResult(professorRetornado));

            var nomeProfessor = await _cargoService.RetornarProfessor(1);

            nomeProfessor.Should().Be($"Cargo {cargoRetornado.Numero}");
        }

        [Fact(DisplayName = "Retornar Professor - Deve retornar nome do professor vinculado ao cargo")]
        [Trait("Unitários", "Cargo Service")]
        public async Task CargoService_RetornarProfessor_DeveRetornarNomeProfessorVinculado()
        {
            var cargoRetornado = new Cargo();
            var professorRetornado = new Professor { Nome = "Professor Teste" };

            _mockRepositorio.Setup(lnq => lnq.Consultar(It.IsAny<Expression<Func<Cargo, bool>>>())).Returns(Task.FromResult(cargoRetornado));
            _mockRepositorio.Setup(lnq => lnq.ConsultarProfessor(It.IsAny<int>())).Returns(Task.FromResult(professorRetornado));

            var nomeProfessor = await _cargoService.RetornarProfessor(1);

            nomeProfessor.Should().Be(professorRetornado.Nome);
        }
    }

}
