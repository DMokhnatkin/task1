using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO
{
    [DataContract]
    public class MeteringDTO : IDTO<IMetering>
    {
        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public float Latitude { get; set; }

        [DataMember]
        public float Longitude { get; set; }

        [DataMember]
        public IDictionary<Guid, IDTO<ISensorValue>> SensorValues { get; set; } = new Dictionary<Guid, IDTO<ISensorValue>>();

        public MeteringDTO()
        {
            
        }

        public MeteringDTO(IMetering model)
        {
            MapFromModel(model);
        }

        public void MapFromModel(IMetering model)
        {
            Time = model.Time;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            SensorValues = model.SensorValues.ToDictionary(k => k.Key, v => SensorsContainer.MapToDTORuntime(v.Value));
        }
    }
}
