using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class SpeedSensorValueDTO : ISensorValue
    {
        public float Speed { get; set; }
    }
}
