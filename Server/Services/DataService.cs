using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
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
            {
                MemoryStream str = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MyData>));
                ser.WriteObject(str, data);
                str.Position = 0;
                logger.Info("teminal(id={0}) called method SendData. Data : {1}", terminalId, new StreamReader(str).ReadToEnd());
            }
            return "OK";
        }
    }
}
