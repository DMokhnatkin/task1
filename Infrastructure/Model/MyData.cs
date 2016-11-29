using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    [DataContract]
    public class MyData
    {
        [DataMember]
        public DateTime Time;

        [DataMember]
        public float Latitude;
        [DataMember]
        public float Longitude;

        [DataMember]
        public float Speed;
        [DataMember]
        public bool IsEngineEnable;
        [DataMember]
        public float Mileage;
    }
}
