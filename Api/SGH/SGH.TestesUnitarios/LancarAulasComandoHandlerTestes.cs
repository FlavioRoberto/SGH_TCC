using FluentAssertions;
using MediatR;
using Moq;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar;
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
            true.Should().BeFalse();

            var comando = new LancarAulasComando(new List<Reserva>());

            _validadorMock.Setup(lnq => lnq.Validar(comando)).Returns("Erro encontrado!");

            var handler = new LancarAulasComandoHandler(_mediatorMock.Object, _validadorMock.Object);
            var resultado = await handler.Handle(comando, default);

            resultado.TemErro().Should().BeTrue();
            resultado.GetErros().Should().Be("Erro encontrado!");

            _validadorMock.Verify(lnq => lnq.Validar(comando), Times.Once);
            _mediatorMock.Verify(lnq => lnq.Send(It.IsAny<CriarAulaComando>(), It.IsAny<CancellationToken>()), Times.Never);

        }

    }
}
