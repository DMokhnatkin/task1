using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// Service for get statistics
    /// </summary>
    [ServiceContract]
    public interface ITerminalsService
    {
        [OperationContract]
        [ServiceKnownType(typeof(TerminalStatus))]
        [ServiceKnownType(typeof(Metering))]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/status",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<TerminalStatus> GetCurStatus();
    }
}
