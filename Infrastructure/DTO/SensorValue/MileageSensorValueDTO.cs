using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class MileageSensorValueDTO : ISensorValue
    {
        public Guid SensorId { get; set; }

        public float Mileage { get; set; }
    }
}
