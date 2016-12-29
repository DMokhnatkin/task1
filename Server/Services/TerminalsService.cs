using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;
using Infrastructure.Model.Sensors.Types;
using Microsoft.Practices.Unity;
using Server.Data;
using Server.Data.Repository;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class TerminalsService : ITerminalsService
    {
        private ServerDbContext _db = MyUnityContainer.Instance.Resolve<ServerDbContext>("db");
        private readonly ITerminalsRepository _terminalsRepository = MyUnityContainer.Instance.Resolve<ITerminalsRepository>();
        private readonly IMeteringRepository _meteringsRepository = MyUnityContainer.Instance.Resolve<IMeteringRepository>();

        public bool IsAlive()
        {
            return true;
        }

        public List<TerminalStatus> GetCurStatus()
        {
            var z = _terminalsRepository
                .GetTerminalIds()
                .Select(x => new TerminalStatus
                {
                    TerminalId = x,
                    LastMetering = _meteringsRepository.GetLastMetering(x)
                })
                .ToList();
            return z;
        }
    }
}
