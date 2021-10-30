using SGH.Dominio.Core.Events;
using SGH.Dominio.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Servicos
{
    public class BusServiceTeste : IBusService
    {
        public Task AdicionarNaFila<T>(T request) where T : Message
        {
            return Task.CompletedTask;
        }

        public Task EscutarNaFila<T>(string messageId, Action<T> onMessage, CancellationToken stoppingToken) where T : Message
        {
            return Task.CompletedTask;
        }
    }
}
