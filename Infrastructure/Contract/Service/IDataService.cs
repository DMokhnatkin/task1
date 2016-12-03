using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// DataService for work with data
    /// </summary>
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [ServiceKnownType(typeof(DataPoint))]
        // TODO: get known types from SensorsRep in runtime. I don't know why It dosn't work: ServiceKnownType("GetSensorTypes", typeof(SensorsRep))
        [ServiceKnownType(typeof(EngineSensor))]
        [ServiceKnownType(typeof(SpeedSensor))]
        [ServiceKnownType(typeof(MileageSensor))]
        [ServiceKnownType(typeof(SpeedSensor))]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/SendData/{terminalId}",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SendData(string terminalId, List<IDataPoint> data);
    }
}
