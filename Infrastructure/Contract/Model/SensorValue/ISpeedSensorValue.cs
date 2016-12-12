
namespace Infrastructure.Contract.Model.SensorValue
{
    public interface ISpeedSensorValue : ISensorValue
    {
        float SpeedKmh { get; set; }
    }
}
