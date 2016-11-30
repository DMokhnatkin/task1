using System;

namespace Infrastructure.Contract.Model
{
    public interface IDataPoint
    {
        DateTime Time { get; set; }

        float Latitude { get; set; }
        float Longitude { get; set; }

        float Speed { get; set; }

        bool IsEngineEnable { get; set; }

        float Mileage { get; set; }
    }
}
