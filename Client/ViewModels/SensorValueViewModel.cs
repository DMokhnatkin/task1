using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;
using Infrastructure.Model;

namespace Client.ViewModels
{
    internal class SensorValueViewModel : ViewModelBase
    {
        private ISensorValue _sensorValue;

        public object Value => _sensorValue.GetValue;

        public string SensorName => SensorsRep.GetSensorTypeInfo(SensorsRep.GetGuid(_sensorValue.GetType())).SensorName;

        public string Units => SensorsRep.GetSensorTypeInfo(SensorsRep.GetGuid(_sensorValue.GetType())).Units;

        public SensorValueViewModel(ISensorValue sensorValue)
        {
            _sensorValue = sensorValue;
        }
    }
}
