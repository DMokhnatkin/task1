using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model.Dto.Reports;

namespace Infrastructure.Contract.Service
{
    [ServiceContract]
    public interface IReportService
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
        ReportDto BuildReport(ReportSettingsDto settings);
    }
}
