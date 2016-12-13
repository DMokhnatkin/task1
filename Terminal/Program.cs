using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using Terminal.Communication.Proxy;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;
using Terminal.Model;
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
        private static Emulator _emulator;

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
                            { SensorsContainer.GetGuid<IEngineSensorValue>(), new EngineDTO(new EngineSensorValue() { IsTurnedOn = true}) },
                            { SensorsContainer.GetGuid<IMileageSensorValue>(), new MileageDTO() { MileageKm = 0.100f} },
                            { SensorsContainer.GetGuid<ISpeedSensorValue>(), new SpeedDTO() { SpeedKmh = 80} },
                        }
                    },
                    new MeteringDTO()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, (points - 1) * timeout / points),
                        Latitude = 56.20015f,
                        Longitude = 36.2f,
                        SensorValues =
                        {
                            { SensorsContainer.GetGuid<IEngineSensorValue>(), new EngineDTO() { IsTurnedOn = true} },
                            { SensorsContainer.GetGuid<IMileageSensorValue>(), new MileageDTO() { MileageKm = 0.122f} },
                            { SensorsContainer.GetGuid<ISpeedSensorValue>(), new SpeedDTO() { SpeedKmh = 80} },
                        }
                    },
                    new MeteringDTO()
                    {
                        Time = DateTime.Now - new TimeSpan(0, 0, 0, 0, (points - 2) * timeout / points),
                        Latitude = 56.2003f,
                        Longitude = 36.2f,
                        SensorValues =
                        {
                            { SensorsContainer.GetGuid<IEngineSensorValue>(), new EngineDTO() { IsTurnedOn = true} },
                            { SensorsContainer.GetGuid<IMileageSensorValue>(), new MileageDTO() { MileageKm = 0.144f} },
                            { SensorsContainer.GetGuid<ISpeedSensorValue>(), new SpeedDTO() { SpeedKmh = 80} },
                        }
                    },
                };*/

                var data = new List<MeteringDTO>()
                {
                    new MeteringDTO(_emulator.GetNext(new TimeSpan()))
                };

                AuthorizationServiceProxy authorizationProxy = new AuthorizationServiceProxy();
                authorizationProxy.Login(_terminalId);
                DataServiceProxy dataProxy = new DataServiceProxy();
                dataProxy.SendData(_terminalId, data);
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

            _emulator = new Emulator(new Mettering());
            Timer timer = new Timer((state) => { ReferToService(addr); }, null, 0, timeout);

            Console.WriteLine("Terminal is run. Press any key to stop");
            Console.ReadKey();
            timer.Dispose();
        }
    }
}
