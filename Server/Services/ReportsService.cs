using System;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single,
        IncludeExceptionDetailInFaults = true)]
    public class ReportsService : IReportService
    {
        /// <inheritdoc />
        public ReportDto BuildReport(ReportSettingsDto settings)
        {
            var rep = new Report();
            rep.ReportSettings.TerminalId = "2";
            rep.ReportSettings.StartDateTime = DateTime.Now;
            rep.ReportSettings.EndDateTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);

            rep.Values.SetValue(DynamicPropertyManagers.Reports.Mileage, 123.4f);
            rep.Values.SetValue(DynamicPropertyManagers.Reports.MaxSpeed, 100f);

            ReportDto res = ReportDto.Wrap(rep);
            return res;
        }
    }
}
