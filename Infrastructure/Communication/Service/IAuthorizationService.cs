using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Communication.Service
{
    /// <summary>
    /// DataService for a authorization
    /// </summary>
    [ServiceContract]
    public interface IAuthorizationService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Login/{terminalId}")]
        void Login(string terminalId);

        bool IsLogged(string terminalId);
    }
}
