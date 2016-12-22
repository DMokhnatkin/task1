using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Server.Data;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class TerminalsService : ITerminalsService
    {
        private ServerDbContext _db = (ServerDbContext)MyUnityContainer.Instance.Resolve(typeof(ServerDbContext), "db");

        public List<TerminalStatus> GetCurStatus()
        {
            return new List<TerminalStatus>()
            {
                new TerminalStatus()
                {
                    TerminalId = "test1",
                    LastMetering = new Metering()
                    {
                        Latitude = 59.918116f,
                        Longitude = 30.346666f,
                        Time = DateTime.Now
                    }
                },
                new TerminalStatus()
                {
                    TerminalId = "test2",
                    LastMetering = new Metering()
                    {
                        Latitude = 59.918116f,
                        Longitude = 30.348666f,
                        Time = DateTime.Now
                    }
                },
                new TerminalStatus()
                {
                    TerminalId = "test3",
                    LastMetering = new Metering()
                    {
                        Latitude = 60.918116f,
                        Longitude = 27.348666f,
                        Time = DateTime.Now
                    }
                }
            };
        }
    }
}
