using Api.Servicos.Email;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message);
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Execute(string toEmail, string subject, string message)
        {
            try
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Email, "SGH - UEMG")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.Bcc.Add(new MailAddress(_emailSettings.CcoEmail));

                mail.Subject = "SGH - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(_emailSettings.Dominio, _emailSettings.Porta))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Senha);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
