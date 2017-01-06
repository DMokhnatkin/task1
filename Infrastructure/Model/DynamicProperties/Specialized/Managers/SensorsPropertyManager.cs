
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Infrastructure.Model.DynamicProperties.Specialized.Managers
{
    public class SensorsPropertyManager : PropertyManagerBase<SensorProperty>
    {
        [Property]
        public SensorProperty IsEngineRunning { get; } = 
            new SensorProperty("Engine on", typeof(bool));

        [Property]
        public SensorProperty SpeedKmh { get; } =
            new SensorProperty("SpeedKmh", typeof(float), "kmh");

        [Property]
        public SensorProperty MileageKm { get; } =
            new SensorProperty("MileageKm", typeof(float), "Km");

        internal SensorsPropertyManager()
        {
            InitializeProps();
        }
    }
}
