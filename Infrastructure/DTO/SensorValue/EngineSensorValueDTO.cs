using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class EngineSensorValueDTO : ISensorValue
    {
        public bool IsTurnedOn { get; set; }
    }
}
