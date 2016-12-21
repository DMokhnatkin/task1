using Infrastructure.Contract.Service;
using Microsoft.Practices.Unity;
using NLog;
using Server.Services;

namespace Server
{
    public class MyUnityContainer
    {
        public static readonly UnityContainer Instance = new UnityContainer();

        static MyUnityContainer()
        {
            Instance.RegisterType<IAuthorizationService, AuthorizationService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ITerminalsService, TerminalsService>(new ContainerControlledLifetimeManager());
        }
    }
}
