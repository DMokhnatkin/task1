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
    [ServiceKnownType(nameof(SensorsRep.GetKnownTypes), typeof(SensorsRep))]
    public interface IDataService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Metering))]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/SendData/{terminalId}",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SendData(string terminalId, List<IMetering> data);
    }
}
