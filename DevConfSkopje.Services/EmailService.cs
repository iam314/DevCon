using DevConfSkopje.Services.Contracts;
using System.Net.Mail;
using DevConfSkopje.Services.Helpers;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Web;
using System.Collections.Generic;

namespace DevConfSkopje.Services
{
    public class EmailService : IEmailService
    {
        public void SendCorfimation(string emailTo, string pathToEmailTemplate)
        {
            //var request = HttpContext.Current.Request;
            //string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
            //SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("devconskopje@gmail.com", "D3vC0nD3v");
            //client.Port = 587;
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //var messageBody = File.ReadAllText(pathToEmailTemplate);
            //messageBody = messageBody.Replace("policylink", baseUrl + "Privacy");
            //messageBody = messageBody.Replace("unsubscribelink", baseUrl + "unsubscribe?email=" + emailTo);

            //MailMessage mailMessage = new MailMessage();

            //mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
            //mailMessage.To.Add(emailTo);
            ////mailMessage.Bcc.Add("istanchev@melontech.com");
            ////mailMessage.Bcc.Add("mkazandzieva@melontech.com");
            ////mailMessage.Bcc.Add("rmateva@melontech.com");
            ////mailMessage.Bcc.Add("pbakovski@@melontech.com");
            ////mailMessage.Bcc.Add("psvarc@melontech.com");

            //mailMessage.Subject = "Registration for DevCon 2019 | Skopje";

            //AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

            //mailMessage.IsBodyHtml = true;
            //mailMessage.AlternateViews.Add(view);

            //client.Send(mailMessage);

            using (var client = new SmtpClient(ConfigurationSettings.SmtpServer, ConfigurationSettings.SmtpPort))
            {
                var request = HttpContext.Current.Request;
                string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
                var messageBody = File.ReadAllText(pathToEmailTemplate);
                messageBody = messageBody.Replace("policylink", baseUrl + "Privacy");
                messageBody = messageBody.Replace("unsubscribelink", baseUrl + "unsubscribe?email=" + emailTo);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
                mailMessage.To.Add(emailTo);

                //mailMessage.Bcc.Add("mkazandzieva@melontech.com");
                //mailMessage.Bcc.Add("rmateva@melontech.com");
                //mailMessage.Bcc.Add("pbakovski@melontech.com");
                //mailMessage.Bcc.Add("psvarc@melontech.com");

                mailMessage.Subject = "Registration for DevCon 2019 | Skopje";

                AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

                mailMessage.IsBodyHtml = true;
                mailMessage.AlternateViews.Add(view);

                client.Send(mailMessage);
            };
        }

        public void SendFeedbackMessages(List<string> emails, string pathToEmailTemplate)
        {
            var request = HttpContext.Current.Request;
            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("devconskopje@gmail.com", "D3vC0nD3v");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            foreach (string email in emails)
            {
                var messageBody = File.ReadAllText(pathToEmailTemplate);
                messageBody = messageBody.Replace("policylink", baseUrl + "Privacy");
                messageBody = messageBody.Replace("unsubscribelink", baseUrl + "unsubscribe?email=" + email);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
                mailMessage.To.Add(email);
                //mailMessage.Bcc.Add("istanchev@melontech.com");
                //mailMessage.Bcc.Add("mkazandzieva@melontech.com");
                //mailMessage.Bcc.Add("rmateva@melontech.com");
                //mailMessage.Bcc.Add("pbakovski@@melontech.com");
                //mailMessage.Bcc.Add("psvarc@melontech.com");

                mailMessage.Subject = "Feedback for DevCon 2019 | Skopje";

                AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

                mailMessage.IsBodyHtml = true;
                mailMessage.AlternateViews.Add(view);

                client.Send(mailMessage); 
            }

            //using (var client = new SmtpClient(ConfigurationSettings.SmtpServer, ConfigurationSettings.SmtpPort))
            //{
            //    var request = HttpContext.Current.Request;
            //    string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
            //    foreach (string email in emails)
            //    {
            //        var messageBody = File.ReadAllText(pathToEmailTemplate);
            //        messageBody = messageBody.Replace("policylink", baseUrl + "Privacy");
            //        messageBody = messageBody.Replace("unsubscribelink", baseUrl + "unsubscribe?email=" + email);

            //        MailMessage mailMessage = new MailMessage();

            //        mailMessage.From = new MailAddress(ConfigurationSettings.SenderEmail);
            //        mailMessage.To.Add(email);

            //        //mailMessage.Bcc.Add("mkazandzieva@melontech.com");
            //        //mailMessage.Bcc.Add("rmateva@melontech.com");
            //        //mailMessage.Bcc.Add("pbakovski@melontech.com");
            //        //mailMessage.Bcc.Add("psvarc@melontech.com");

            //        mailMessage.Subject = "Feedback for DevCon 2019 | Skopje";

            //        AlternateView view = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);

            //        mailMessage.IsBodyHtml = true;
            //        mailMessage.AlternateViews.Add(view);

            //        client.Send(mailMessage);
            //    }
            //};
        }
    }
}
