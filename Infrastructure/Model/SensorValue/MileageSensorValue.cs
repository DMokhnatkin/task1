using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.Model.SensorValue
{
    class MileageSensorValue : IMileageSensorValue
    {
        /// <inheritdoc />
        public float MileageKm { get; set; }
    }
}
