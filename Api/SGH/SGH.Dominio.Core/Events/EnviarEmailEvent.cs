using MediatR;

namespace SGH.Dominio.Core.Events
{
    public class EnviarEmailEvent : Message
    {
        public string Mensagem { get; private set; }
        public string Assunto { get; private set; }
        public string Email { get; private set; }

        public EnviarEmailEvent(string email, string assunto, string mensagem)
        {
            Email = email;
            Assunto = assunto;
            Mensagem = mensagem;
        }

    }
}
