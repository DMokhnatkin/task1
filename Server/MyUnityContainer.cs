using Infrastructure.Contract.Service;
using Microsoft.Practices.Unity;
using Server.Data;
using Server.Data.Repository;
using Server.Services;

namespace Server
{
    public class MyUnityContainer
    {
        public static readonly UnityContainer Instance = new UnityContainer();

        static MyUnityContainer()
        {
            // Services
            Instance.RegisterType<IAuthorizationService, AuthorizationService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ITerminalsService, TerminalsService>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<IReportService, ReportsService>(new ContainerControlledLifetimeManager());

            // Repositories
            Instance.RegisterType<IMeteringRepository, MeteringRepository>(new ContainerControlledLifetimeManager());
            Instance.RegisterType<ITerminalsRepository, TerminalsRepository>(new ContainerControlledLifetimeManager());

            // Db context
            Instance.RegisterType<ServerDbContext>("db", new TransientLifetimeManager());
        }
    }
}
