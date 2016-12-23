using Infrastructure.Contract.Model;

namespace Infrastructure.Model.SensorValue
{
    public class EngineSensorValue : ISensorValue
    {
        public bool IsTurnedOn { get; set; }

        public object GetValue => IsTurnedOn;
    }
}
