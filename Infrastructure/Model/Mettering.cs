using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Contract.Model;
using Infrastructure.DTO;

namespace Terminal.Model
{
    class Mettering : IMetering
    {
        /// <inheritdoc />
        public DateTime Time { get; set; }

        /// <inheritdoc />
        public float Latitude { get; set; }

        /// <inheritdoc />
        public float Longitude { get; set; }

        /// <inheritdoc />
        public IDictionary<Guid, ISensorValue> SensorValues { get; } = new Dictionary<Guid, ISensorValue>();

        public void AddSensorValue<TSensorValue>(TSensorValue value)
            where TSensorValue : ISensorValue
        {
            SensorValues[SensorsContainer.GetGuid<TSensorValue>()] = value;
        }
    }
}
