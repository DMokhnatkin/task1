using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model
{
    [DataContract]
    public class DataPoint : IDataPoint
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
