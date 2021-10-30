using SGH.Dominio.Core.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Services
{
    public interface IBusService
    {
        Task AdicionarNaFila<T>(T request) where T : Message;
        Task EscutarNaFila<T>(string messageId, Action<T> onMessage, CancellationToken stoppingToken) where T : Message;
    }
}
