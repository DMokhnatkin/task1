using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.Model.SensorValue
{
    class SpeedSensorValue : ISpeedSensorValue
    {
        /// <inheritdoc />
        public float SpeedKmh { get; set; }
    }
}
