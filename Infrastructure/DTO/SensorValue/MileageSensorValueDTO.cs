using System.Runtime.Serialization;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps IMileageSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class MileageSensorValueDTO : IMileageSensorValue
    {
        public MileageSensorValueDTO()
        {
            
        }

        public MileageSensorValueDTO(IMileageSensorValue model)
        {
            this.MileageKm = model.MileageKm;
        }

        [DataMember]
        public float MileageKm { get; set; }
    }
}
