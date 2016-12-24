using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors.Types
{
    [SensorType("ca761232ed4211cebacd00aa0057b224", SensorName = "Mileage", Units = "km")]
    public class MileageSensorValue : ISensorValue
    {
        public float MileageKm { get; set; }

        public object GetValue => MileageKm;
    }
}
