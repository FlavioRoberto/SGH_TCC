using EasyNetQ;
using MediatR;
using SGH.Dominio.Core.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Events
{
    public class EnviarEmailEventHandler : IRequestHandler<EnviarEmailEvent>
    {
        IBus _bus;

        public async Task<Unit> Handle(EnviarEmailEvent request, CancellationToken cancellationToken)
        {
            var bus = RabbitHutch.CreateBus("host=sgh.rabbitmq");

            bus.PubSub.Publish(request);

            return Unit.Value;
        }
    }
}
