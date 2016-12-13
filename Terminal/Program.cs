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

        private static Metering prev;
        const int points = 3;

        static void ReferToService(string serviceAddress)
        {
            try
            {
                if (prev == null)
                    prev = Emulator.GetRandom();
                List<IMetering> data = new List<IMetering>();
                for (int i = 0; i < points; i++)
                {
                    prev = Emulator.GetNext(prev, new TimeSpan(0, 0, 0, 0, timeout/points), (float)new Random().NextDouble() * 60);
                    data.Add(prev);
                }

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

            Timer timer = new Timer((state) => { ReferToService(addr); }, null, 0, timeout);

            Console.WriteLine("Terminal is run. Press any key to stop");
            Console.ReadKey();
            timer.Dispose();
        }
    }
}
