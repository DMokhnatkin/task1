using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Microsoft.Practices.Unity;
using NLog;
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

        static void Main(string[] args)
        {
            StartServices();
            Console.WriteLine("All services were started. Press enter to stop server.");
            Console.ReadLine();
            StopServices();
        }
    }
}
