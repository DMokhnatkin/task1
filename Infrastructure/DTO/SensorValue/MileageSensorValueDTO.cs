using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class MileageSensorValueDTO : ISensorValue
    {
        public Int64 MeteringId { get; set; }

        public float Mileage { get; set; }
    }
}
