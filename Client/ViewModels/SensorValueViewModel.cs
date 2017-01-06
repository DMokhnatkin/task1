using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Client.ViewModels
{
    internal class SensorValueViewModel : ViewModelBase
    {
        private SensorProperty _property;
        private PropertiesCollection _collection;

        public object Value => _collection.GetValue(_property);

        public string SensorName => _property.Name;

        public string Units => _property.Unit;

        public SensorValueViewModel(SensorProperty prop, PropertiesCollection coll)
        {
            _property = prop;
            _collection = coll;
        }
    }
}
