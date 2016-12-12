using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using Terminal.Communication.Proxy;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;
using Terminal.Model.SensorValue;

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
                /*
                List<IMetering> sample = new List<IMetering>()
                {
                    new MeteringDTO()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, points * timeout / points),
                        Latitude = 56.2f,
                        Longitude = 36.2f,
                        SensorValues =
                        {
                            { SensorsRep.GetGuid<IEngineSensorValue>(), new EngineIdto(new EngineSensorValue() { IsTurnedOn = true}) },
                            { SensorsRep.GetGuid<IMileageSensorValue>(), new MileageIdto() { MileageKm = 0.100f} },
                            { SensorsRep.GetGuid<ISpeedSensorValue>(), new SpeedIdto() { SpeedKmh = 80} },
                        }
                    },
                    new MeteringDTO()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, (points - 1) * timeout / points),
                        Latitude = 56.20015f,
                        Longitude = 36.2f,
                        SensorValues =
                        {
                            { SensorsRep.GetGuid<IEngineSensorValue>(), new EngineIdto() { IsTurnedOn = true} },
                            { SensorsRep.GetGuid<IMileageSensorValue>(), new MileageIdto() { MileageKm = 0.122f} },
                            { SensorsRep.GetGuid<ISpeedSensorValue>(), new SpeedIdto() { SpeedKmh = 80} },
                        }
                    },
                    new MeteringDTO()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, (points - 2) * timeout / points),
                        Latitude = 56.2003f,
                        Longitude = 36.2f,
                        SensorValues =
                        {
                            { SensorsRep.GetGuid<IEngineSensorValue>(), new EngineIdto() { IsTurnedOn = true} },
                            { SensorsRep.GetGuid<IMileageSensorValue>(), new MileageIdto() { MileageKm = 0.144f} },
                            { SensorsRep.GetGuid<ISpeedSensorValue>(), new SpeedIdto() { SpeedKmh = 80} },
                        }
                    },
                };*/

                AuthorizationServiceProxy authorizationProxy = new AuthorizationServiceProxy();
                authorizationProxy.Login(_terminalId);
                DataServiceProxy dataProxy = new DataServiceProxy();
                //dataProxy.SendData(_terminalId, sample);
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
