using System.Threading.Tasks;

namespace SGH.Api.Servicos.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
