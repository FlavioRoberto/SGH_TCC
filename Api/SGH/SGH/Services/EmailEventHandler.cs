using EasyNetQ;
using Microsoft.Extensions.Hosting;
using SGH.Dominio.Core.Events;
using SGH.Email.Services.Email;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Email.Services
{
    public class EmailEventHandler : BackgroundService
    {
        private IBus _bus;
        private EmailService _emailService;

        public EmailEventHandler(EmailService emailService)
        {
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=sgh.rabbitmq");
            _bus.PubSub.Subscribe<EnviarEmailEvent>("PagamentoService", ProcessarEnvio);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
            _bus.Dispose();
        }

        private void ProcessarEnvio(EnviarEmailEvent emailEvent)
        {
            _emailService.Enviar(emailEvent.Email, emailEvent.Assunto, emailEvent.Mensagem);
        }
    }
}
