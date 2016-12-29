﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using AutoMapper;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;
using Infrastructure.Model.Sensors.Types;
using Microsoft.Practices.Unity;
using NLog;
using Server.Data;
using Server.Data.DAO;
using Server.Services;
using Unity.Wcf;

namespace Server
{
    class Program
    {
        static readonly List<ServiceDescription> serviceDescriptions = new List<ServiceDescription>()
        {
            new ServiceDescription()
            {
                Uri = new Uri("http://localhost:2224/auth"),
                DataContractType = typeof(IAuthorizationService),
                ServiceHost = null
            },
            new ServiceDescription()
            {
                Uri = new Uri("http://localhost:2224/data"),
                DataContractType = typeof(IDataService),
                ServiceHost = null
            },
            new ServiceDescription()
            {
                Uri = new Uri("http://localhost:2224/terminals"),
                DataContractType = typeof(ITerminalsService),
                ServiceHost = null
            }
        };

        static void StartServices()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            foreach (var serviceDescription in serviceDescriptions)
            {
                serviceDescription.ServiceHost = new UnityServiceHost(
                    MyUnityContainer.Instance, 
                    MyUnityContainer.Instance.Resolve(serviceDescription.DataContractType).GetType(), // Get type of resolved object
                    serviceDescription.Uri);
                try
                {
                    serviceDescription.ServiceHost.Open();
                    logger.Info("service {0} was started", serviceDescription.DataContractType);
                }
                catch (Exception)
                {
                    logger.Fatal("failed start {0}", serviceDescription.DataContractType);
                    throw;
                }
            }
        }

        static void StopServices()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            foreach (var serviceDescription in serviceDescriptions)
            {
                try
                {
                    serviceDescription.ServiceHost.Close();
                    logger.Info("service {0} was stopped", serviceDescription.ServiceHost);
                }
                catch (Exception)
                {
                    logger.Fatal("failed stop {0}", serviceDescription.ServiceHost);
                    throw;
                }
            }
        }

        static void InitializeDAOMapper()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Metering, MeteringDAO>().ReverseMap();
                    cfg.CreateMap<ISensorValue, SensorValueDAO>()
                        .ForMember(dest => dest.Value, opt => opt.MapFrom(src => DAOHelper.ObjectToByteArray(src)));
                }
            );
        }

        static void Main(string[] args)
        {
            InitializeDAOMapper();
            StartServices();
            Console.WriteLine("All services were started. Press enter to stop server.");
            Console.ReadLine();
            StopServices();
        }
    }
}
