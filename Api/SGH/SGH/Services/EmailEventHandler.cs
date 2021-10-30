using Microsoft.Extensions.Hosting;
using SGH.Dominio.Core.Events;
using SGH.Dominio.Core.Services;
using SGH.Email.Services.Email;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Email.Services
{
    public class EmailEventHandler : BackgroundService
    {
        private IBusService _bus;
        private EmailService _emailService;

        public EmailEventHandler(EmailService emailService, IBusService busService)
        {
            _emailService = emailService;
            _bus = busService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            await _bus.EscutarNaFila<EnviarEmailEvent>("EnvioEmail", emailEvent => {
                _emailService.Enviar(emailEvent.Email, emailEvent.Assunto, emailEvent.Mensagem);
            }, stoppingToken);
        }
    }
}
