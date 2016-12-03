using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.DAO.Sensors
{
    class SpeedSensorValueDAO
    {
        [Key]
        public Guid SensorId { get; set; }

        public float Speed { get; set; }
    }
}
