using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Contract.Model;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;

namespace Infrastructure.Contract.Service
{
    /// <summary>
    /// DataService for work with data
    /// </summary>
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [ServiceKnownType(typeof(MeteringDTO))]
        // TODO: get known types from SensorsContainer in runtime. I don't know why It dosn't work: ServiceKnownType("GetSensorValContractTypes", typeof(SensorsContainer))
        [ServiceKnownType(typeof(EngineDTO))]
        [ServiceKnownType(typeof(SpeedDTO))]
        [ServiceKnownType(typeof(MileageDTO))]
        [ServiceKnownType(typeof(SpeedDTO))]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/SendData/{terminalId}",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SendData(string terminalId, List<MeteringDTO> data);
    }
}
