
namespace Infrastructure.Model
{
    public class SensorTypeInfo
    {
        public string SensorName { get; private set; }

        public string Units { get; private set; }

        public SensorTypeInfo(string sensorName, string units)
        {
            SensorName = sensorName;
            Units = units;
        }
    }
}
