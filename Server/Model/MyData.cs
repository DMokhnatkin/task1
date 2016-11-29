using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    [DataContract]
    public class MyData
    {
        [DataMember]
        public string TerminalId;

        [DataMember]
        public DateTime Time;

        [DataMember]
        public DateTime Latitude;
        [DataMember]
        public DateTime Longitude;

        [DataMember]
        public float Speed;
        [DataMember]
        public bool IsEngineEnable;
        [DataMember]
        public bool Mileage;
    }
}
