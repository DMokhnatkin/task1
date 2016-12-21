﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.Serialization;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model
{
    [DataContract]
    public class Metering : IMetering
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
        public IDictionary<Guid, ISensorValue> SensorValues { get; set; } = new Dictionary<Guid, ISensorValue>();

        public void AddSensorValue<TSensorValue>(TSensorValue value)
            where TSensorValue : ISensorValue
        {
            SensorValues[SensorsRep.GetGuid<TSensorValue>()] = value;
        }

        public TSensorValue GetSensorValue<TSensorValue>()
            where TSensorValue : ISensorValue
        {
            return (TSensorValue)SensorValues[SensorsRep.GetGuid<TSensorValue>()];
        }
    }
}
