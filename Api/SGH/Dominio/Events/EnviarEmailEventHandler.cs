using MediatR;
using SGH.Dominio.Core.Events;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Events
{
    public class EnviarEmailEventHandler : IRequestHandler<EnviarEmailEvent>
    {
        IBusService _busService;

        public EnviarEmailEventHandler(IBusService busService)
        {
            _busService = busService;
        }

        public Task<Unit> Handle(EnviarEmailEvent request, CancellationToken cancellationToken)
        {
            _busService.AdicionarNaFila(request);
            return Task.FromResult(Unit.Value);
        }
    }
}
