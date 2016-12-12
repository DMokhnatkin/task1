﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AutoMapper;
using Infrastructure.BL.ValidateMetering;
using Infrastructure.BL.ValidateMetering.Validators;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Service;
using Infrastructure.DTO;
using Microsoft.Practices.Unity;
using NLog;
using Server.Data;
using Server.Data.DAO;
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

        public void SendData(string terminalId, List<IMetering> data)
        {
            if (!_authorization.IsLogged(terminalId))
            {
                logger.Warn("SendData was forbidden. Terminal(id={0}) is not logged", terminalId);
                //throw new WebFaultException<string>("Call Login before call SendData", HttpStatusCode.Unauthorized);
            }
            else
            {
                /*
                MeteringValidateManager _validateManager = new MeteringValidateManager(
                    new List<IMeteringValidator>()
                    {
                        new MileageDistanceValidator(10),
                        new SpeedMileageTimeValidator(20)
                    });
                try
                {
                    _validateManager.Validate(data);
                }
                catch (Exception e)
                {
                    logger.Warn(e);
                    return;
                }*/

                // Save in db
                foreach (var metering in data)
                {
                    _meteringRepository.SaveMetering(metering);
                }

                MemoryStream str = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(
                    typeof(List<IMetering>), 
                    new List<Type>()
                    {
                        typeof(MeteringDTO)
                    }
                    .Concat(SensorsRep.GetSensorValContractTypes())
                    );
                ser.WriteObject(str, data);
                str.Position = 0;
                logger.Info("teminal(id={0}) called method SendData. Data : {1}", terminalId, new StreamReader(str).ReadToEnd());
            }
        }
    }
}
