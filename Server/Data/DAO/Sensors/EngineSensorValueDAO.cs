﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.DAO.Sensors
{
    public class EngineSensorValueDAO
    {
        [Key]
        public Guid SensorId { get; set; }

        public bool IsEnabled { get; set; }
    }
}
