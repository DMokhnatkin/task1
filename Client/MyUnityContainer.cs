using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Communication.Proxy;
using Common.Communication.ProxyWrappers;
using Infrastructure.Contract.Service;
using Microsoft.Practices.Unity;

namespace Client
{
    public class MyUnityContainer
    {
        public static readonly UnityContainer Instance = new UnityContainer();

        static MyUnityContainer()
        {
            // Proxies
            Instance.RegisterType<ITerminalsService, TerminalServiceProxyWrapper>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IReportService, ReportServiceProxy>(new ContainerControlledLifetimeManager());
        }
    }
}
