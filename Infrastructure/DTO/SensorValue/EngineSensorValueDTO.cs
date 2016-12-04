using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.DTO.SensorValue
{
    public class EngineSensorValueDTO : ISensorValue
    {
        public Int64 MeteringId { get; set; }

        public bool IsTurnedOn { get; set; }
    }
}
