using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Infrastructure.Contract.Model;

namespace Server.Data.DAO
{
    class MeteringDAO : IMetering
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public DateTime Time { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public virtual List<MeteringSensorValueRelationDAO> SensorValueRelations { get; set; }

        [NotMapped]
        public IDictionary<Guid, ISensorValue> SensorValues {
            get { return SensorValueRelations.ToDictionary(x => x.SensorGuid, y => (ISensorValue)y.SensorValue); }
        }
    }
}
