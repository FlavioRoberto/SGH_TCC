using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Email
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<EmailConfiguracoes> configuracoes)
        {
            _configuracoes = configuracoes.Value;
        }

        public EmailConfiguracoes _configuracoes { get; }

        public Task Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                Executar(email, assunto, mensagem);
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Executar(string toEmail, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuracoes.Email, "SGH - UEMG")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.Bcc.Add(new MailAddress(_configuracoes.CcoEmail));

                mail.Subject = "SGH - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(_configuracoes.Dominio, _configuracoes.Porta))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_configuracoes.Email, _configuracoes.Senha);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                throw new Exception("Não foi possível enviar o e-mail para o usuário, verifique se o e-mail e a senha do aplicativo são válidas.", ex);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
