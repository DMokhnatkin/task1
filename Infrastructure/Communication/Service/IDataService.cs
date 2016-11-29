using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infrastructure.Model;

namespace Infrastructure.Communication.Service
{
    /// <summary>
    /// DataService for work with data
    /// </summary>
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/SendData/{terminalId}",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string SendData(string terminalId, List<MyData> data);
    }
}
