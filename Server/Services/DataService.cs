using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using NLog;
using Server.Model;

namespace WCFServer.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class DataService : IDataService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAuthorizationService _authorization = MyUnityContainer.Instance.Resolve<IAuthorizationService>();

        public string SendData(string terminalId)
        {
            if (!_authorization.IsLogged(terminalId))
                logger.Warn("SendData was forbidden. Terminal(id={0}) is not logged", terminalId);
            else
                logger.Info("teminal(id={0}) called method SendData", terminalId);
            return "OK";
        }
    }
}
