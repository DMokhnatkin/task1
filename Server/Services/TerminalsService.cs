using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto;
using Infrastructure.Model.Dto.Meterings;
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

        public bool Ping()
        {
            return true;
        }

        public List<TerminalStatusDto> GetCurStatus()
        {
            var z = _terminalsRepository
                .GetTerminalIds()
                .Select(x => new TerminalStatusDto
                {
                    TerminalId = x,
                    LastMetering = new MeteringDto(_meteringsRepository.GetLastMetering(x).Result)
                })
                .ToList();
            return z;
        }
    }
}
