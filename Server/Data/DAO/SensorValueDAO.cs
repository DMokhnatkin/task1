using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Contract.Model;

namespace Server.Data.DAO
{
    class SensorValueDAO : ISensorValue
    {
        [Key]
        public Guid SensorId { get; set; }
    }
}
