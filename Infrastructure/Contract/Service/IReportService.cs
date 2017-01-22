using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Infrastructure.Model.Dto.Reports;

namespace Infrastructure.Contract.Service
{
    [ServiceContract]
    public interface IReportService : IPingAvailable
    {
        /// <summary>
        /// Build report
        /// </summary>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "build",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task<ReportDto> BuildReport(ReportSettingsDto settings);
    }
}
