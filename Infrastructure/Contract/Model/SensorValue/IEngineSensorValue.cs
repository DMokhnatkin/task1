
namespace Infrastructure.Contract.Model.SensorValue
{
    public interface IEngineSensorValue : ISensorValue
    {
        bool IsTurnedOn { get; set; }
    }
}
