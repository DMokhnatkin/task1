using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors.Types
{
    [SensorType("ca761232ed4211cebacd00aa0057b223", SensorName = "Engine", Units = "")]
    public class EngineSensorValue : ISensorValue
    {
        public bool IsTurnedOn { get; set; }

        public object ObjValue
        {
            get { return IsTurnedOn; }
            set { IsTurnedOn = (bool)value; }
        }
    }
}
