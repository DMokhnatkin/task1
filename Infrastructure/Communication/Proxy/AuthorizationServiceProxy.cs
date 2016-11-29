using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Communication.Service;

namespace Infrastructure.Communication.Proxy
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
