using EasyNetQ;
using Microsoft.Extensions.Configuration;
using SGH.Dominio.Core.Events;
using SGH.Dominio.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Servicos
{
    public class BusService : IBusService
    {
        private IConfiguration _configuration;
        private IBus _bus;

        public BusService(IConfiguration configuration)
        {
            _configuration = configuration;
            var host = _configuration["RabbitMq"];
            _bus = RabbitHutch.CreateBus($"host={host}");
        }


        public async Task AdicionarNaFila<T>(T request) where T : Message
        {
            await _bus.PubSub.PublishAsync(request).ConfigureAwait(false);
        }

        public async Task EscutarNaFila<T>(string messageId, Action<T> onMessage, CancellationToken stoppingToken) where T : Message
        {
            _bus.PubSub.Subscribe<T>(messageId, onMessage);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
            _bus.Dispose();
        }
    }
}
