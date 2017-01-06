using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;

namespace Common.Communication.Proxy
{
    public class ReportServiceProxy :
        ClientBase<IReportService>,
        IReportService
    {
        /// <inheritdoc />
        public ReportDto BuildReport(ReportSettingsDto settings)
        {
            return Channel.BuildReport(settings);
        }
    }
}
