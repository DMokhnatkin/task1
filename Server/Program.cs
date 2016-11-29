using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Communication.Service;
using Microsoft.Practices.Unity;
using NLog;
using Server;
using Server.Services;

namespace Server
{
    class Program
    {
        static readonly Dictionary<Uri, Type> serviceDescriptions = new Dictionary<Uri, Type>()
        {
            {new Uri("http://localhost:2224/auth"), typeof(AuthorizationService)},
            {new Uri("http://localhost:2224/data"), typeof(DataService)}
        };

        static void StartServices()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            foreach (var serviceDescription in serviceDescriptions)
            {
                ServiceHost host = new ServiceHost(serviceDescription.Value, serviceDescription.Key);
                try
                {
                    host.Open();
                    logger.Info("service {0} was started", serviceDescription.Value);
                }
                catch (Exception)
                {
                    logger.Fatal("failed start {0}", serviceDescription.Value);
                    throw;
                }
            }
        }

        static void StopServices()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            foreach (var serviceDescription in serviceDescriptions)
            {
                ServiceHost host = new ServiceHost(serviceDescription.Value, serviceDescription.Key);
                try
                {
                    host.Close();
                    logger.Info("service {0} was stopped", serviceDescription.Value);
                }
                catch (Exception)
                {
                    logger.Fatal("failed stop {0}", serviceDescription.Value);
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
