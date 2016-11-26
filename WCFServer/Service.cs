using System;
using System.ServiceModel;
using NLog;

namespace WCFServer
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class Service : IService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string SendData(string terminalId)
        {
            logger.Info("teminal(id={0}) called method SendData", terminalId);
            return "OK";
        }
    }
}
