using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServer;

namespace WCFServer
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class Service : IService
    {
        public string SendData(string terminalId)
        {
            Console.WriteLine(string.Format("info: {0}; terminalId = {1}", DateTime.Now, terminalId));
            return "OK";
        }
    }
}
