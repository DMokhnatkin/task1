using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.Model.SensorValue
{
    class EngineSensorValue : IEngineSensorValue
    {
        /// <inheritdoc />
        public bool IsTurnedOn { get; set; }
    }
}
