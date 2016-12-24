using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// Service for get statistics
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(nameof(SensorsRep.GetKnownTypes), typeof(SensorsRep))]
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
