using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors.Types
{
    [SensorType("ca761232ed4211cebacd00aa0057b225", SensorName = "Speed", Units = "kmh")]
    public class SpeedSensorValue : ISensorValue
    {
        public float SpeedKmh { get; set; }

        public object ObjValue
        {
            get { return SpeedKmh; }
            set { SpeedKmh = (float)value; }
        }
    }
}
