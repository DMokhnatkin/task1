using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer.Services
{
    /// <summary>
    /// DataService for a authorization
    /// </summary>
    [ServiceContract]
    interface IAuthorizationService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Login/{terminalId}")]
        void Login(string terminalId);

        bool IsLogged(string terminalId);
    }
}
