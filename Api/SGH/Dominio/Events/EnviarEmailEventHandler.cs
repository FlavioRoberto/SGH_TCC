using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using SGH.Dominio.Core.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Events
{
    public class EnviarEmailEventHandler : IRequestHandler<EnviarEmailEvent>
    {
        IBus _bus;
        private IConfiguration _configuration;

        public EnviarEmailEventHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Unit> Handle(EnviarEmailEvent request, CancellationToken cancellationToken)
        {
            var host = _configuration["RabbitMq"];
            var bus = RabbitHutch.CreateBus($"host={host}");

            bus.PubSub.Publish(request);

            return Unit.Value;
        }
    }
}
