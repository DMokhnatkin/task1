using System;
using System.ServiceModel;
using Unity.Wcf;

namespace Server
{
    public class ServiceDescription
    {
        public Uri Uri { get; set; }

        public Type DataContractType { get; set; }

        public ServiceHost ServiceHost { get; set; }
    }
}
