using System.Threading.Tasks;

namespace Api.Servicos.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
