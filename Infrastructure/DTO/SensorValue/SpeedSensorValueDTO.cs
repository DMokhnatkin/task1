using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class SpeedSensorValueDTO : ISensorValue
    {
        public Guid SensorId { get; set; }

        public float Speed { get; set; }
    }
}
