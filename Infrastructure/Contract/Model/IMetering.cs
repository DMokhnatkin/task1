using System;
using Infrastructure.Model.DynamicProperties;

namespace Infrastructure.Contract.Model
{
    public interface IMetering
    {
        string TerminalId { get; set; }

        DateTime Time { get; set; }

        float Latitude { get; set; }
        float Longitude { get; set; }

        PropertiesCollection SensorValues { get; set; }
    }
}
