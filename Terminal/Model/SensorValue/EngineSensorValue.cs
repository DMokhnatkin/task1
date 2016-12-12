using Infrastructure.Contract.Model.SensorValue;

namespace Terminal.Model.SensorValue
{
    class EngineSensorValue : IEngineSensorValue
    {
        /// <inheritdoc />
        public bool IsTurnedOn { get; set; }
    }
}
