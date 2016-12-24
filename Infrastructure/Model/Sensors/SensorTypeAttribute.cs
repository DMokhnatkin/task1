
using System;

namespace Infrastructure.Model.Sensors
{
    public class SensorTypeAttribute : Attribute
    {
        public Guid Guid { get; private set; }

        public string SensorName { get; set; }

        public string Units { get; set; }

        public SensorTypeAttribute(string guid)
        {
            Guid = new Guid(guid);
        }
    }
}
