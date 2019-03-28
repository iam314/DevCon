using System;
using System.Configuration;

namespace DevConfSkopje.Services.Helpers
{
    public static class ConfigurationSettings
    {
        public static string SmtpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPServer"];
            }
        }

        public static int SmtpPort
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            }
        }

        public static string SenderEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["Sender"];
            }
        }
    }
}
