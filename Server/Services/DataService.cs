using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Communication.Service;
using Microsoft.Practices.Unity;
using NLog;
using Infrastructure.Model;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class DataService : IDataService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAuthorizationService _authorization = MyUnityContainer.Instance.Resolve<IAuthorizationService>();

        public string SendData(string terminalId, List<MyData> data)
        {
            if (!_authorization.IsLogged(terminalId))
                logger.Warn("SendData was forbidden. Terminal(id={0}) is not logged", terminalId);
            else
                logger.Info("teminal(id={0}) called method SendData", terminalId);
            return "OK";
        }
    }
}
