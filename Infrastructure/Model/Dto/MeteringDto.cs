using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Contract.Model;
using Infrastructure.Model.DynamicProperties.Specialized;

namespace Infrastructure.Model.Dto
{
    [DataContract]
    public class MeteringDto
    {
        [DataMember]
        public string TerminalId { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public float Latitude { get; set; }
        [DataMember]
        public float Longitude { get; set; }

        [DataMember]
        public Dictionary<string, object> _sensorValues { get; set; } = new Dictionary<string, object>();

        public MeteringDto(IMetering metering)
        {
            TerminalId = metering.TerminalId;
            Time = metering.Time;
            Latitude = metering.Latitude;
            Longitude = metering.Longitude;

            foreach (var z in metering.SensorValues)
            {
                _sensorValues.Add(z.Key.Name, z.Value);
            }
        }

        public IMetering ToBo()
        {
            Metering res = new Metering();
            res.TerminalId = TerminalId;
            res.Time = Time;
            res.Latitude = Latitude;
            res.Longitude = Longitude;

            foreach (var z in _sensorValues)
            {
                res.SensorValues.SetValue(
                    DynamicPropertyManagers.Sensors.GetProperty(z.Key),
                    z.Value);
            }
            return res;
        }
    }
}
