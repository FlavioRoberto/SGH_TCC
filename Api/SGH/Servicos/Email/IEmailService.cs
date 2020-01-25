using System.Threading.Tasks;

namespace SGH.Dominio.Core.Email
{
    public interface IEmailService
    {
        Task Enviar(string email, string assunto, string mensagem);
    }
}
