using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
