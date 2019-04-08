namespace DevConfSkopje.Services.Contracts
{
    public interface IEmailService
    {
        void SendCorfimation(string emailTo, string pathToEmailTemplate, string pathToImage, string pathToLogo);
    }
}
