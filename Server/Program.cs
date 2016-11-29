using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WCFServer;
using WCFServer.Services;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost _serviceHost1 = new WebServiceHost(typeof(AuthorizationService), new Uri("http://127.0.0.1:2224/auth"));
            WebServiceHost _serviceHost2 = new WebServiceHost(typeof(DataService), new Uri("http://127.0.0.1:2224/data"));
            _serviceHost1.Open();
            _serviceHost2.Open();
            Console.ReadKey();
            _serviceHost1.Close();
            _serviceHost2.Close();
        }
    }
}
