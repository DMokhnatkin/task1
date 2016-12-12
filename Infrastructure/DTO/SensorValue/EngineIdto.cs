using System.Runtime.Serialization;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps IEngineSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class EngineIdto : IDTO<IEngineSensorValue>
    {
        [DataMember]
        public bool IsTurnedOn { get; set; }

        /// <inheritdoc />
        public void MapFromModel(IEngineSensorValue model)
        {
            this.IsTurnedOn = model.IsTurnedOn;
        }
    }
}
