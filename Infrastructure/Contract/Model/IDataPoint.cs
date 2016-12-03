using System;
using System.Collections.Generic;

namespace Infrastructure.Contract.Model
{
    public interface IDataPoint
    {
        DateTime Time { get; set; }

        float Latitude { get; set; }
        float Longitude { get; set; }

        IDictionary<Guid, ISensorValue> SensorValues { get; }
    }
}
