using System;
using System.Runtime.Serialization;
using Infrastructure.Contract.Model;
using Infrastructure.Model.DynamicProperties;

namespace Infrastructure.Model
{
    public class Metering : IMetering
    {
        public string TerminalId { get; set; }

        public DateTime Time { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public PropertiesCollection SensorValues { get; set; } = new PropertiesCollection();
    }
}
