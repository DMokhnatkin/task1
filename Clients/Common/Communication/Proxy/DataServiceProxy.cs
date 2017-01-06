using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto;
using Infrastructure.Model.Dto.Meterings;

namespace Common.Communication.Proxy
{
    public class DataServiceProxy :
        ClientBase<IDataService>, 
        IDataService
    {
        public void SendData(string terminalId, List<MeteringDto> data)
        {
            base.Channel.SendData(terminalId, data);
        }
    }
}
