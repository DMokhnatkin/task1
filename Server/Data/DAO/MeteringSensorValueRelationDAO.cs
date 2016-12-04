using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.DAO
{
    class MeteringSensorValueRelationDAO
    {
        public Int64 Id { get; set; }

        public Int64 MeteringId { get; set; }
        [ForeignKey("MeteringId")]
        public MeteringDAO Metering { get; set; }

        public Int64 SensorValueId { get; set; }
        [ForeignKey("SensorValueId")]
        public SensorValueDAO SensorValue { get; set; }

        public Guid SensorGuid { get; set; }
    }
}
