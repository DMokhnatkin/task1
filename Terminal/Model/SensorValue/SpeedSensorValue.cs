using Infrastructure.Contract.Model.SensorValue;

namespace Terminal.Model.SensorValue
{
    class SpeedSensorValue : ISpeedSensorValue
    {
        /// <inheritdoc />
        public float SpeedKmh { get; set; }
    }
}
