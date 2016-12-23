using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Contract.Model;

namespace Server.Data.DAO
{
    class SensorValueDAO : ISensorValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        // TODO: now sensor values will be serialized in db. We can create ef class for each sensor value type. It will increase permomance.
        public byte[] Value { get; set; }

        [NotMapped]
        public object GetValue => Value;
    }
}
