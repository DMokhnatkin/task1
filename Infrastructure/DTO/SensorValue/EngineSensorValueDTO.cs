using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class EngineSensorValueDTO : ISensorValue
    {
        public Guid SensorId { get; set; }

        public bool IsTurnedOn { get; set; }
    }
}
