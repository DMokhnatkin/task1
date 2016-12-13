using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using Terminal.Communication.Proxy;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.SensorValue;

namespace Terminal
{
    class Program
    {
        /// <summary>
        /// Id of current terminal
        /// </summary>
        private static string _terminalId;
        private static int timeout;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void ReferToService(string serviceAddress)
        {
            try
            {
                const int points = 3;
                List<IMetering> sample = new List<IMetering>()
                {
                    new Metering()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, points * timeout / points),
                        Latitude = 0,
                        Longitude = 0,
                        SensorValues =
                        {
                            { SensorsRep.GetGuid<EngineSensorValue>(), new EngineSensorValue() { IsTurnedOn = true} },
                            { SensorsRep.GetGuid<MileageSensorValue>(), new MileageSensorValue() { Mileage = 100} },
                            { SensorsRep.GetGuid<SpeedSensorValue>(), new SpeedSensorValue() { Speed = 100} },
                        }
                    },
                    new Metering()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, points * timeout / points),
                        Latitude = 0,
                        Longitude = 0,
                    },
                    new Metering()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, points * timeout / points),
                        Latitude = 0,
                        Longitude = 0,
                    },
                };

                AuthorizationServiceProxy authorizationProxy = new AuthorizationServiceProxy();
                authorizationProxy.Login(_terminalId);
                DataServiceProxy dataProxy = new DataServiceProxy();
                dataProxy.SendData(_terminalId, sample);
                logger.Info("Data sent");
            }
            catch (Exception e)
            {
                logger.Warn("Can't call method: {0}", e.Message);
            }
        }

        static void Main(string[] args)
        {
            // Read and parse arguments
            if (args.Length != 3)
            {
                Exception e = new ArgumentException("3 arguments are required (Terminal id; service address; timeout)");
                logger.Fatal(e);
                throw e;
            }

            _terminalId = args[0];
            string addr = args[1];
            try
            {
                timeout = Convert.ToInt32(args[2]);
            }
            catch (Exception)
            {
                Exception e = new ArgumentException("Timeout must be a double const");
                logger.Fatal(e);
                throw e;
            }

            Timer timer = new Timer((state) => { ReferToService(addr); }, null, 0, timeout);

            Console.WriteLine("Terminal is run. Press any key to stop");
            Console.ReadKey();
            timer.Dispose();
        }
    }
}
