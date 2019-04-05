using DevConfSkopje.Services.Contracts;
using System.Net.Mail;
using DevConfSkopje.Services.Helpers;
using System.IO;

namespace DevConfSkopje.Services
{
    public class EmailService : IEmailService
    {
        public void SendCorfimation(string emailTo, string pathToEmailTemplate)
        {
            using (var client = new SmtpClient(ConfigurationSettings.SmtpServer, ConfigurationSettings.SmtpPort))
            {
                var messageBody = File.ReadAllText(pathToEmailTemplate);
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
                mailMessage.To.Add(emailTo);
                mailMessage.Subject = "Registration for DevCon 2019 | Skopje";
                mailMessage.Body = messageBody;

                client.Send(mailMessage);
            };
        }
    }
}
