using System;
using System.Net;
using System.Threading;

namespace Terminal
{
    class Program
    {
        /// <summary>
        /// Id of current terminal
        /// </summary>
        private static string _terminalId;

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
                Console.WriteLine("Can't call method:");
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            // Read and parse arguments
            if (args.Length != 3)
            {
                throw new ArgumentException("3 arguments are required (Terminal id; service address; timeout)");
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
                throw new ArgumentException("Timeout must be a double const");
            }

            Timer timer = new Timer((state) => { ReferToService(addr); }, null, 0, timeout);

            Console.WriteLine("Terminal is run. Press any key to stop");
            Console.ReadKey();
            timer.Dispose();
        }
    }
}
