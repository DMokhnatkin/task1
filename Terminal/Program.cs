using System;
using System.Net;
using System.Threading;
using NLog;

namespace Terminal
{
    class Program
    {
        /// <summary>
        /// Id of current terminal
        /// </summary>
        private static string _terminalId;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void ReferToService(string serviceAddress)
        {
            try
            {
                string requestAddress = string.Format("{0}/{1}/{2}", serviceAddress, "SendData", _terminalId);
                WebRequest req = WebRequest.Create(requestAddress);
                WebResponse response = req.GetResponse();
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
            int timeout;
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
