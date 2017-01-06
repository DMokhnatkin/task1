using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.Reports;

namespace Server.Services
{
    [ServiceBehavior(
         InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Single)]
    public class ReportsService : IReportService
    {
        /// <inheritdoc />
        public ReportDto BuildReport(ReportSettingsDto settings)
        {
            ReportDto res = new ReportDto(new Report());
            return res;
        }
    }
}
