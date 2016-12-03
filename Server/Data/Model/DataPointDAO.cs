using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Contract.Model;
using Infrastructure.Model;

namespace Server.Data.Model
{
    class DataPointDAO
    {
        [Key]
        public Guid Id;

        public DateTime Time { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Speed { get; set; }

        public bool IsEngineEnable { get; set; }

        public float Mileage { get; set; }
    }
}
