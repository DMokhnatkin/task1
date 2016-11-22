using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using WCFServer;

namespace WCFServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            WebServiceHost _serviceHost = new WebServiceHost(service, new Uri("http://127.0.0.1:2224/myService"));
            _serviceHost.Open();
            Console.ReadKey();
            _serviceHost.Close();
        }
    }
}
