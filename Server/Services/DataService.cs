using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Dto;
using Microsoft.Practices.Unity;
using NLog;
using Server.Data.Repository;

namespace Server.Services
{
    [ServiceBehavior(
         InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Single)]
    public class DataService : IDataService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IAuthorizationService _authorization = MyUnityContainer.Instance.Resolve<IAuthorizationService>();

        private MeteringRepository _meteringRepository = new MeteringRepository();

        public void SendData(string terminalId, List<MeteringDto> data)
        {
            if (!_authorization.IsLogged(terminalId))
            {
                logger.Warn("SendData was forbidden. Terminal(id={0}) is not logged", terminalId);
                //throw new WebFaultException<string>("Call Login before call SendData", HttpStatusCode.Unauthorized);
            }
            else
            {
                foreach (var z in data)
                {
                    var metering = z.ToBo();
                    try
                    {
                        _meteringRepository.SaveMetering(metering);
                    }
                    catch (Exception e)
                    {
                        logger.Warn(e);
                        return;
                    }
                }

                MemoryStream str = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(
                    typeof(List<MeteringDto>), 
                    new List<Type>()
                    {
                        typeof(MeteringDto)
                    }
                    );
                ser.WriteObject(str, data);
                str.Position = 0;
                logger.Info("teminal(id={0}) called method SendData. Data : {1}", terminalId, new StreamReader(str).ReadToEnd());
            }
        }
    }
}
