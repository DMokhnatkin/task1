using Infrastructure.Contract.Model.SensorValue;

namespace Terminal.Model.SensorValue
{
    class MileageSensorValue : IMileageSensorValue
    {
        /// <inheritdoc />
        public float MileageKm { get; set; }
    }
}
