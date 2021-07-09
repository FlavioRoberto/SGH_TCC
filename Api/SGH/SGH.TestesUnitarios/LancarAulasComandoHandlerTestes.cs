using FluentAssertions;
using MediatR;
using Moq;
using SGH.Dominio.Core;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [Collection(nameof(UsuarioCollection))]
    public class LancarAulasComandoHandlerTestes
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IValidador<LancarAulasComando>> _validadorMock;

        public LancarAulasComandoHandlerTestes()
        {
            _mediatorMock = new Mock<IMediator>();
            _validadorMock = new Mock<IValidador<LancarAulasComando>>();
        }


        [Fact(DisplayName = "Lancar aulas - Deve retornar lista com erros do validador")]
        [Trait("Unitários", "Cargo Service")]
        public async Task LancarAulaHandler_Handle_DeveRetornarListaComErros()
        {
            var comando = new LancarAulasComando(new List<Reserva>());

            _validadorMock.Setup(lnq => lnq.Validar(comando)).Returns("Erro encontrado!");

            var handler = new LancarAulasComandoHandler(_mediatorMock.Object, _validadorMock.Object);
            var resultado = await handler.Handle(comando, default);

            resultado.TemErro().Should().BeTrue();
            resultado.GetErros().Should().Be("Erro encontrado!");

            _validadorMock.Verify(lnq => lnq.Validar(comando), Times.Once);
            _mediatorMock.Verify(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()), Times.Never);

        }

        [Fact(DisplayName = "Lancar aulas - Deve enviar a notificação de cadastro de reservas")]
        [Trait("Unitários", "Cargo Service")]
        public async Task LancarAulaHandler_Handle_DeveNotificarCadastroReservas()
        {
            _mediatorMock.Setup(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.FromResult(new Resposta<AulaViewModel>(new AulaViewModel(), "")));

            var comando = new LancarAulasComando(new List<Reserva> { 
                new Reserva("Terça", "07:00"),
                new Reserva("Segunda", "07:00")
            });

            var handler = new LancarAulasComandoHandler(_mediatorMock.Object, _validadorMock.Object);
            var resultado = await handler.Handle(comando, default);

            resultado.TemErro().Should().BeFalse();
            resultado.GetErros().Should().BeEmpty();
            resultado.GetResultado().Should().BeEmpty();

            _validadorMock.Verify(lnq => lnq.Validar(comando), Times.Once);
            _mediatorMock.Verify(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

        }

        [Fact(DisplayName = "Lancar aulas - Deve retornar erro ao notificar reserva")]
        [Trait("Unitários", "Cargo Service")]
        public async Task LancarAulaHandler_Handle_DeveRetornarErroAoNotificarReserva()
        {
            _mediatorMock.Setup(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.FromResult(new Resposta<AulaViewModel>(new AulaViewModel(), "Erro")));

            var comando = new LancarAulasComando(new List<Reserva> {
                new Reserva("Terça", "07:00")
            });

            var handler = new LancarAulasComandoHandler(_mediatorMock.Object, _validadorMock.Object);
            var resultado = await handler.Handle(comando, default);

            resultado.TemErro().Should().BeFalse();
            resultado.GetErros().Should().BeEmpty();
            resultado.GetResultado().Should().HaveCount(1);
            resultado.GetResultado().Should().Contain("Erro");

            _validadorMock.Verify(lnq => lnq.Validar(comando), Times.Once);
            _mediatorMock.Verify(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()), Times.Once);

        }

    }
}
