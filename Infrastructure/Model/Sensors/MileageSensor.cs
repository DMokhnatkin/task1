using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors
{
    public class MileageSensor : ISensorValue
    {
        public Guid SensorId { get; set; }

        private float _mileage;

        public object Value
        {
            get { return _mileage; }
            set { _mileage = (float)value; }
        }

        public float CastedValue
        {
            get { return _mileage; }
            set { _mileage = value; }
        }
    }
}
