using System.Threading.Tasks;

namespace SGH.Dominio.Core.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
