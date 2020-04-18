using FluentAssertions;
using Moq;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class RemoverHorarioTeste
    {
        readonly UsuarioTestsFixture _usuarioTestsFixture;

        public RemoverHorarioTeste(UsuarioTestsFixture usuarioTestsFixture)
        {
            _usuarioTestsFixture = usuarioTestsFixture;
        }

        [Fact(DisplayName = "Remover horário - Deve retornar resposta com erro de código não pode ser vazio")]
        [Trait("Unitários", "Usuário")]
        public async Task Remover_Horario_DeveRetornarMensagemCodigoNaoPodeSerVazio()
        {
            var mockRepositorio = new Mock<IHorarioAulaRepositorio>();
            var mockValidador = new RemoverHorarioComandoValidador(mockRepositorio.Object);
            var handle = new RemoverHorarioComandoHandler(mockRepositorio.Object, mockValidador);
            var comando = new RemoverHorarioComando();

            var resultado = await handle.Handle(comando, CancellationToken.None);

            resultado.TemErro().Should().BeTrue();

            resultado.GetErros().Trim().Should().Be("O código do horário não foi informado.");
        }

        [Fact(DisplayName = "Remover horário - Deve retornar resposta com erro de código não encontrado")]
        [Trait("Unitários", "Usuário")]
        public async Task Remover_Horario_DeveRetornarMensagemCodigoNaoEncontrado()
        {
            var mockRepositorio = new Mock<IHorarioAulaRepositorio>();
            var mockValidador = new RemoverHorarioComandoValidador(mockRepositorio.Object);
            var handle = new RemoverHorarioComandoHandler(mockRepositorio.Object, mockValidador);
            var comando = new RemoverHorarioComando { CodigoHorario = 1 };

            var resultado = await handle.Handle(comando, CancellationToken.None);

            resultado.TemErro().Should().BeTrue();

            resultado.GetErros().Trim().Should().Be($"Não foi encontrado um horário com o código {comando.CodigoHorario}.");

            mockRepositorio.Verify(lnq => lnq.Contem(It.IsAny<Expression<Func<HorarioAula, bool>>>()), Times.Once);
        }

        [Fact(DisplayName = "Remover horário - Deve remover com sucesso")]
        [Trait("Unitários", "Usuário")]
        public async Task Remover_Horario_DeveRemoverComSucesso()
        {
            var mockRepositorio = new Mock<IHorarioAulaRepositorio>();
            var mockValidador = new RemoverHorarioComandoValidador(mockRepositorio.Object);
            var handle = new RemoverHorarioComandoHandler(mockRepositorio.Object, mockValidador);
            var comando = new RemoverHorarioComando { CodigoHorario = 1 };

            mockRepositorio.Setup(lnq => lnq.Contem(It.IsAny<Expression<Func<HorarioAula, bool>>>())).Returns(Task.FromResult(true));

            mockRepositorio.Setup(lnq => lnq.Remover(It.IsAny<Expression<Func<HorarioAula, bool>>>())).Returns(Task.FromResult(true));

            var resultado = await handle.Handle(comando, CancellationToken.None);

            resultado.TemErro().Should().BeFalse();

            resultado.GetErros().Should().BeEmpty();
            
            mockRepositorio.Verify(lnq => lnq.Contem(It.IsAny<Expression<Func<HorarioAula, bool>>>()), Times.Once);
            mockRepositorio.Verify(lnq => lnq.Remover(It.IsAny<Expression<Func<HorarioAula, bool>>>()), Times.Once);

        }

    }
}
