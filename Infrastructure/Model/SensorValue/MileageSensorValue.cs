using Infrastructure.Contract.Model;

namespace Infrastructure.Model.SensorValue
{
    public class MileageSensorValue : ISensorValue
    {
        public float MileageKm { get; set; }

        public object GetValue => MileageKm;
    }
}
