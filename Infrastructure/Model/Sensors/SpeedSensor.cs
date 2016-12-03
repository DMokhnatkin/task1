using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors
{
    public class SpeedSensor : ISensorValue
    {
        public Guid SensorId { get; set; }

        private float _value;

        public object Value {
            get { return (object)_value; }
            set { _value = (float)value; }
        }

        public float CastedValue
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
