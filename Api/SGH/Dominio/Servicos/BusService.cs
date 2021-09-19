using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using SGH.Dominio.Core.Services;

namespace SGH.Dominio.Services.Servicos
{
    public class BusService : IBusService
    {
        IBusService _bus;
        private IConfiguration _configuration;

        public BusService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void AdicionarNaFila(IRequest request)
        {
            _bus.AdicionarNaFila(request);

            var host = _configuration["RabbitMq"];
            var bus = RabbitHutch.CreateBus($"host={host}");

            bus.PubSub.Publish(request);
        }
    }
}
