using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class TerminalsService : ITerminalsService
    {
        public List<TerminalStatus> GetCurStatus()
        {

            return new List<TerminalStatus>()
            {
                new TerminalStatus()
                {
                    TerminalId = "test1",
                    LastMetering = new Metering()
                    {
                        Latitude = 5,
                        Longitude = 6,
                        Time = DateTime.Now
                    }
                },
                new TerminalStatus()
                {
                    TerminalId = "test2",
                    LastMetering = new Metering()
                    {
                        Latitude = 9,
                        Longitude = 7,
                        Time = DateTime.Now
                    }
                }
            };
        }
    }
}
