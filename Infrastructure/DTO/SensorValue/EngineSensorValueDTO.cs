using System.Runtime.Serialization;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps IEngineSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class EngineSensorValueDTO : IEngineSensorValue
    {
        public EngineSensorValueDTO()
        {
            
        }

        public EngineSensorValueDTO(IEngineSensorValue model)
        {
            this.IsTurnedOn = model.IsTurnedOn;
        }

        [DataMember]
        public bool IsTurnedOn { get; set; }
    }
}
