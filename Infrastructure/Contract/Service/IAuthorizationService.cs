using System.ServiceModel;
using System.ServiceModel.Web;

namespace Infrastructure.Contract.Service
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
