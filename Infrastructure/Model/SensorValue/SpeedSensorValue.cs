using Infrastructure.Contract.Model;

namespace Infrastructure.Model.SensorValue
{
    public class SpeedSensorValue : ISensorValue
    {
        public float SpeedKmh { get; set; }

        public object GetValue => SpeedKmh;
    }
}
