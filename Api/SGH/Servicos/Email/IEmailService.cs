using System.Threading.Tasks;

namespace Api.Aplicacao.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
