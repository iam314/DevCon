using DevConfSkopje.Services.Contracts;
using System.Net.Mail;
using DevConfSkopje.Services.Helpers;
using System.IO;
using System.Net.Mime;
using System.Net;

namespace DevConfSkopje.Services
{
    public class EmailService : IEmailService
    {
        public void SendCorfimation(string emailTo, string pathToEmailTemplate)
        {
            //SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("devconskopje@gmail.com", "D3vC0nD3v");
            //client.Port = 587;
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //var messageBody = File.ReadAllText(pathToEmailTemplate);
            //MailMessage mailMessage = new MailMessage();

            //mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
            //mailMessage.To.Add(emailTo);
            //mailMessage.Bcc.Add("mkazandzieva@melontech.com");
            //mailMessage.Bcc.Add("rmateva@melontech.com");
            //mailMessage.Bcc.Add("pbakovski@@melontech.com");
            //mailMessage.Bcc.Add("psvarc@melontech.com");

            //mailMessage.Subject = "Registration for DevCon 2019 | Skopje";

            //AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

            //mailMessage.IsBodyHtml = true;
            //mailMessage.AlternateViews.Add(view);

            //client.Send(mailMessage);

            using (var client = new SmtpClient(ConfigurationSettings.SmtpServer, ConfigurationSettings.SmtpPort))
            {
                var messageBody = File.ReadAllText(pathToEmailTemplate);
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
                mailMessage.To.Add(emailTo);

                mailMessage.Bcc.Add("mkazandzieva@melontech.com");
                mailMessage.Bcc.Add("rmateva@melontech.com");
                mailMessage.Bcc.Add("pbakovski@@melontech.com");
                mailMessage.Bcc.Add("psvarc@melontech.com");

                mailMessage.Subject = "Registration for DevCon 2019 | Skopje";

                AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

                mailMessage.IsBodyHtml = true;
                mailMessage.AlternateViews.Add(view);

                client.Send(mailMessage);
            };
        }
    }
}
