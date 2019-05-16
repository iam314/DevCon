using System.Collections.Generic;

namespace DevConfSkopje.Services.Contracts
{
    public interface IEmailService
    {
        void SendCorfimation(string emailTo, string pathToEmailTemplate);
        void SendFeedbackMessages(List<string> emails, string pathToEmailTemplate);
    }
}
