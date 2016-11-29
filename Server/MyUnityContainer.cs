
using Microsoft.Practices.Unity;
using WCFServer.Services;

namespace WCFServer
{
    class MyUnityContainer
    {
        public static readonly UnityContainer Instance = new UnityContainer();

        static MyUnityContainer()
        {
            Instance.RegisterType<IAuthorizationService, AuthorizationService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
        }
    }
}
