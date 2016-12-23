using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model;
using Infrastructure.Model.SensorValue;

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
        // TODO: get known types from SensorsRep in runtime. I don't know why It dosn't work: ServiceKnownType("GetSensorValTypes", typeof(SensorsRep))
        [ServiceKnownType(typeof(EngineSensorValue))]
        [ServiceKnownType(typeof(SpeedSensorValue))]
        [ServiceKnownType(typeof(MileageSensorValue))]
        [ServiceKnownType(typeof(SpeedSensorValue))]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/status",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<TerminalStatus> GetCurStatus();
    }
}
