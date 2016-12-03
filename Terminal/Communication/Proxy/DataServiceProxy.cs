using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Service;

namespace Terminal.Communication.Proxy
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
