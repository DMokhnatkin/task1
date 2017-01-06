using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Client.ViewModels
{
    internal class SensorValueViewModel : ViewModelBase
    {
        private SensorProperty _property;
        private object _val;

        public object Value => _val;

        public string SensorName => _property.Name;

        public string Units => _property.Unit;

        public SensorValueViewModel(SensorProperty prop, object val)
        {
            _property = prop;
            _val = val;
        }
    }
}
