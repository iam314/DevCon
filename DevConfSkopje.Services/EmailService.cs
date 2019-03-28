using DevConfSkopje.Services.Contracts;
using System.Net.Mail;
using DevConfSkopje.Services.Helpers;

namespace DevConfSkopje.Services
{
    public class EmailService : IEmailService
    {
        public void SendCorfimation(string emailTo)
        {
            using (var client = new SmtpClient(ConfigurationSettings.SmtpServer, ConfigurationSettings.SmtpPort))
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
                mailMessage.To.Add(emailTo);
                mailMessage.Subject = "Registration for DevCon 2019 | Skopje";
                mailMessage.Body = $"Congratulations ! \n\n You successfully registered for DevConf 2019 !";

                client.Send(mailMessage);
            };
        }
    }
}
