using System;
using System.Collections.Generic;
using System.Linq;
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
        public string SendData(string terminalId, List<MyData> data)
        {
            return base.Channel.SendData(terminalId, data);
        }
    }
}
