using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model;
using Infrastructure.Model.Dto;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// Service for get statistics
    /// </summary>
    [ServiceContract]
    public interface ITerminalsService : IPingAvailable
    {
        [OperationContract]
        [ServiceKnownType(typeof(TerminalStatusDto))]
        [ServiceKnownType(typeof(Metering))]
        [ServiceKnownType(typeof(List<Metering>))]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/status",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<TerminalStatusDto> GetCurStatus();
    }
}
