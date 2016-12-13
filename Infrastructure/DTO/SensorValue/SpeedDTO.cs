using System.Runtime.Serialization;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps ISpeedSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class SpeedDTO : IDTO<ISpeedSensorValue>
    {
        [DataMember]
        public float SpeedKmh { get; set; }

        /// <inheritdoc />
        public void MapFromModel(ISpeedSensorValue model)
        {
            this.SpeedKmh = model.SpeedKmh;
        }
    }
}
