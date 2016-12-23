using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;

namespace Client.ViewModels
{
    internal class SensorValueViewModel : ViewModelBase
    {
        private ISensorValue _sensorValue;

        public object Value => _sensorValue.GetValue;

        public string SensorName => _sensorValue.GetType().Name;

        public SensorValueViewModel(ISensorValue sensorValue)
        {
            _sensorValue = sensorValue;
        }
    }
}
