using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model.Sensors
{
    public class EngineSensor : ISensorValue
    {
        public Guid SensorId { get; set; }

        private bool _isEnabled;

        public object Value
        {
            get { return _isEnabled; }
            set { _isEnabled = (bool)value; }
        }

        public bool CastedValue
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
    }
}
