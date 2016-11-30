using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Communication.Service;
using Infrastructure.Model;

namespace Infrastructure.Communication.Proxy
{
    public class DataServiceProxy :
        ClientBase<IDataService>, 
        IDataService
    {
        public void SendData(string terminalId, List<IDataPoint> data)
        {
            base.Channel.SendData(terminalId, data);
        }
    }
}
