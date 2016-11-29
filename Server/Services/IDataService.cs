using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCFServer.Services
{
    /// <summary>
    /// DataService for work with data
    /// </summary>
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/SendData/{terminalId}")]
        string SendData(string terminalId);
    }
}
