using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Infrastructure.Contract.Model;

namespace Server.Data.DAO
{
    class MeteringDAO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        public virtual ICollection<MeteringSensorValueRelationDAO> SensorValueRelations { get; set; }
    }
}
