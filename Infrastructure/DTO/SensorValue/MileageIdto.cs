using System.Runtime.Serialization;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;

namespace Infrastructure.DTO.SensorValue
{
    /// <summary>
    /// Wraps IMileageSensorValue for transfer 
    /// </summary>
    [DataContract]
    public class MileageIdto : IDTO<IMileageSensorValue>
    {
        [DataMember]
        public float MileageKm { get; set; }

        /// <inheritdoc />
        public void MapFromModel(IMileageSensorValue model)
        {
            this.MileageKm = model.MileageKm;
        }
    }
}
