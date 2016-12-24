using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Sensors.Types;
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
            var m1 = new Metering()
            {
                Latitude = 59.918116f,
                Longitude = 30.346666f,
                Time = DateTime.Now
            };
            SensorValuesHelper.AddSensorValue(m1, new SpeedSensorValue() { SpeedKmh = 34});

            var m2 = new Metering()
            {
                Latitude = 59.918116f,
                Longitude = 30.348666f,
                Time = DateTime.Now
            };

            var m3 = new Metering()
            {
                Latitude = 60.918116f,
                Longitude = 27.348666f,
                Time = DateTime.Now
            };
            SensorValuesHelper.AddSensorValue(m3, new MileageSensorValue() { MileageKm = 32});
            SensorValuesHelper.AddSensorValue(m3, new SpeedSensorValue() { SpeedKmh = 14});

            return new List<TerminalStatus>()
            {
                new TerminalStatus()
                {
                    TerminalId = "test1",
                    LastMetering = m1
                },
                new TerminalStatus()
                {
                    TerminalId = "test2",
                    LastMetering = m2
                },
                new TerminalStatus()
                {
                    TerminalId = "test3",
                    LastMetering = m3
                }
            };
        }
    }
}
