using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.DAO
{
    class MeteringSensorValueRelationDAO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Int64 MeteringId { get; set; }
        [ForeignKey("MeteringId")]
        public MeteringDAO Metering { get; set; }

        public Int64 SensorValueId { get; set; }
        [ForeignKey("SensorValueId")]
        public SensorValueDAO SensorValue { get; set; }

        public string PropertyName { get; set; }
    }
}
