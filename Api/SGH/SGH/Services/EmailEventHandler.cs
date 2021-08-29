using EasyNetQ;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration _configuration;
        private EmailService _emailService;

        public EmailEventHandler(EmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var host = _configuration["RabbitMq:connection"];
            _bus = RabbitHutch.CreateBus($"host={host}");
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
