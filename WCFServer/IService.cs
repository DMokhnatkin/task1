using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCFServer
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/SendData/{terminalId}")]
        string SendData(string terminalId);
    }
}
