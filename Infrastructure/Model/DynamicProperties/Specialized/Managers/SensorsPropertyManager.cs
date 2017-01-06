
using Infrastructure.Model.DynamicProperties.Specialized.Attributes;

namespace Infrastructure.Model.DynamicProperties.Specialized.Managers
{
    public class SensorsPropertyManager : PropertyManagerBase
    {
        [SensorProperty]
        public Property IsEngineRunning { get; } = 
            new Property("Engine on", typeof(bool));

        [SensorProperty(Unit = "Kmh")]
        public Property SpeedKmh { get; } =
            new Property("SpeedKmh", typeof(float));

        [SensorProperty(Unit = "Km")]
        public Property MileageKm { get; } =
            new Property("Mileage", typeof(float));

        internal SensorsPropertyManager()
        {
            InitializeProps();
        }
    }
}
