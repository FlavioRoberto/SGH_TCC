using System.Threading.Tasks;

namespace SGH.Dominio.Services.Email
{
    public interface IEmailService
    {
        Task Enviar(string email, string assunto, string mensagem);
    }
}
