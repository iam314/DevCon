using System.Collections.Generic;
using System.IO;
using DevConfSkopje.DataModels;
using DevConfSkopje.Services.Contracts;
using OfficeOpenXml;

namespace DevConfSkopje.Services
{
    public class ExportRegistrationsService : IExportRegistrationsService
    {
        public byte[] ExportToExcel(List<ConferenceRegistration> registrations)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("DevConSkopje2019");

                var registrationsWorkSheet = excel.Workbook.Worksheets["DevConSkopje2019"];
                int rowIterator = 1;

                registrationsWorkSheet.Cells[rowIterator, 1].Value = "First name";
                registrationsWorkSheet.Cells[rowIterator, 2].Value = "Last name";
                registrationsWorkSheet.Cells[rowIterator, 3].Value = "Email";
                registrationsWorkSheet.Cells[rowIterator, 4].Value = "Phone";
                
                registrationsWorkSheet.Column(1).Width = 30;
                registrationsWorkSheet.Column(2).Width = 30;
                registrationsWorkSheet.Column(3).Width = 30;
                registrationsWorkSheet.Column(4).Width = 30;

                foreach (var reg in registrations)
                {
                    rowIterator++;

                    registrationsWorkSheet.Cells[rowIterator, 1].Value = reg.FirsName;
                    registrationsWorkSheet.Cells[rowIterator, 2].Value = reg.LastName;
                    registrationsWorkSheet.Cells[rowIterator, 3].Value = reg.Email;
                    registrationsWorkSheet.Cells[rowIterator, 4].Value = reg.PhoneNumber;
                }

                return excel.GetAsByteArray();
            }
        }

        public void LogInvalidRegistration(string directoryPath, ConferenceRegistration registration)
        {
            bool invalidRegistrationsExcelExist = new FileInfo(directoryPath + "DevConfSkopje2019-InvalidRegistrations.xlsx").Exists;
            FileInfo invalidRegistrationsExcel = null;

            if (!invalidRegistrationsExcelExist)
            {
                invalidRegistrationsExcel = new FileInfo(directoryPath + "DevConfSkopje2019-InvalidRegistrations.xlsx");
            }

            using (ExcelPackage excel = new ExcelPackage(invalidRegistrationsExcel))
            {
                ExcelWorksheet invalidRegistrationsWs = null;

                if(excel.Workbook.Worksheets["DevConSkopje2019-Invalid"] == null)
                {
                    invalidRegistrationsWs = excel.Workbook.Worksheets.Add("DevConSkopje2019-Invalid");
                }

                invalidRegistrationsWs.Cells[1, 1].Value = "First name";
                invalidRegistrationsWs.Cells[1, 2].Value = "Last name";
                invalidRegistrationsWs.Cells[1, 3].Value = "Email";
                invalidRegistrationsWs.Cells[1, 4].Value = "Phone";
                invalidRegistrationsWs.Cells[1, 5].Value = "Description";

                int lastRow = invalidRegistrationsWs.Dimension.End.Row;

                lastRow++;

                invalidRegistrationsWs.Column(1).Width = 30;
                invalidRegistrationsWs.Column(2).Width = 30;
                invalidRegistrationsWs.Column(3).Width = 30;
                invalidRegistrationsWs.Column(4).Width = 30;
                invalidRegistrationsWs.Column(5).Width = 30;

                invalidRegistrationsWs.Cells[lastRow, 1].Value = registration.FirsName;
                invalidRegistrationsWs.Cells[lastRow, 2].Value = registration.LastName;
                invalidRegistrationsWs.Cells[lastRow, 3].Value = registration.Email;
                invalidRegistrationsWs.Cells[lastRow, 4].Value = registration.PhoneNumber;
                invalidRegistrationsWs.Cells[lastRow, 5].Value = "Prazno";

                excel.SaveAs(invalidRegistrationsExcel);
            }
        }
    }
}
