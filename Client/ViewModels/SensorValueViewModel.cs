using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;

namespace Client.ViewModels
{
    internal class SensorValueViewModel : ViewModelBase
    {
        private ISensorValue _sensorValue;

        public object Value => _sensorValue.ObjValue;

        public string SensorName => SensorsRep.GetSensorTypeAttribute(_sensorValue.GetType()).SensorName;

        public string Units => SensorsRep.GetSensorTypeAttribute(_sensorValue.GetType()).Units;

        public SensorValueViewModel(ISensorValue sensorValue)
        {
            _sensorValue = sensorValue;
        }
    }
}
