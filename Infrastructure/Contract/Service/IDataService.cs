using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.SensorValue;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// DataService for work with data
    /// </summary>
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Metering))]
        // TODO: get known types from SensorsRep in runtime. I don't know why It dosn't work: ServiceKnownType("GetSensorValTypes", typeof(SensorsRep))
        [ServiceKnownType(typeof(EngineSensorValue))]
        [ServiceKnownType(typeof(SpeedSensorValue))]
        [ServiceKnownType(typeof(MileageSensorValue))]
        [ServiceKnownType(typeof(SpeedSensorValue))]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/SendData/{terminalId}",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SendData(string terminalId, List<IMetering> data);
    }
}
