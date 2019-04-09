using DevConfSkopje.DataModels;
using System.Collections.Generic;

namespace DevConfSkopje.Services.Contracts
{
    public interface IExportRegistrationsService
    {
        byte[] ExportToExcel(List<ConferenceRegistration> registrations);
        void LogInvalidRegistration(string directoryPath, ConferenceRegistration registration);
    }
}
