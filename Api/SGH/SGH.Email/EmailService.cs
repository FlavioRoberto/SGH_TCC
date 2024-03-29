﻿using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SGH.Email.Services.Email
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        private void Executar(string toEmail, string subject, string message)
        {
            try
            {
                var _configuracoes = RetornarConfiguracoes();

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuracoes.Email, "SGH - UEMG")
                };

                mail.To.Add(new MailAddress(toEmail));

                if (!string.IsNullOrEmpty(_configuracoes.CcoEmail))
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
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                throw new Exception($"Não foi possível enviar o e-mail para o usuário, verifique se o e-mail e a senha do aplicativo são válidas. Detalhes: {ex.Message}", ex);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private EmailConfiguracoes RetornarConfiguracoes()
        {
            return new EmailConfiguracoes
            {
                CcoEmail = _configuration["ConfiguracoesEmail:CcoEmail"],
                Dominio = _configuration["ConfiguracoesEmail:Dominio"],
                Email = _configuration["EMAIL"],
                Porta = int.Parse(_configuration["ConfiguracoesEmail:Porta"]),
                Senha = _configuration["EMAIL_SENHA"]
            };
        }

    }
}
