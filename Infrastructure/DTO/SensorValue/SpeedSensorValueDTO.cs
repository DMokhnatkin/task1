using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class SpeedSensorValueDTO : ISensorValue
    {
        public Int64 MeteringId { get; set; }

        public float Speed { get; set; }
    }
}
