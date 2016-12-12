using System.Runtime.Serialization;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps ISpeedSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class SpeedSensorValueDTO : ISpeedSensorValue
    {
        public SpeedSensorValueDTO()
        {
            
        }

        public SpeedSensorValueDTO(ISpeedSensorValue model)
        {
            this.SpeedKmh = model.SpeedKmh;
        }

        [DataMember]
        public float SpeedKmh { get; set; }
    }
}
