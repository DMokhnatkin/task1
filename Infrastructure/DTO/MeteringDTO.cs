using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO
{
    [DataContract]
    public class MeteringDTO : IMetering
    {
        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public float Latitude { get; set; }

        [DataMember]
        public float Longitude { get; set; }

        [DataMember]
        public IDictionary<Guid, ISensorValue> SensorValues { get; set; } = new Dictionary<Guid, ISensorValue>();
    }
}
