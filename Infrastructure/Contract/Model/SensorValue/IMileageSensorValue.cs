
namespace Infrastructure.Contract.Model.SensorValue
{
    public interface IMileageSensorValue : ISensorValue
    {
        float MileageKm { get; set; }
    }
}
