using System.ServiceModel;
using Infrastructure.Contract.Service;

namespace Common.Communication.Proxy
{
    public class AuthorizationServiceProxy : 
        ClientBase<IAuthorizationService>,
        IAuthorizationService
    {
        public void Login(string terminalId)
        {
            base.Channel.Login(terminalId);
        }

        public bool IsLogged(string terminalId)
        {
            return base.Channel.IsLogged(terminalId);
        }
    }
}
