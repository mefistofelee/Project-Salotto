using Salotto.App.Common.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Salotto.App.Common.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body, List<string> ccRecipients = null, List<string> bccRecipients = null);
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public SmtpEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body, List<string> ccRecipients = null, List<string> bccRecipients = null)
        {
            var smtpClient = new SmtpClient
            {
                Host = _emailSettings.SmtpServer,
                Port = _emailSettings.SmtpPort,
                Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.UserName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            if (ccRecipients != null)
            {
                foreach (var cc in ccRecipients)
                {
                    mailMessage.CC.Add(cc);
                }
            }

            if (bccRecipients != null)
            {
                foreach (var bcc in bccRecipients)
                {
                    mailMessage.Bcc.Add(bcc);
                }
            }

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
