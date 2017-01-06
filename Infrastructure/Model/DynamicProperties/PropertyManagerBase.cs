using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.DynamicProperties.Specialized.Managers;

namespace Infrastructure.Model.DynamicProperties
{
    /// <summary>
    /// Property manager stores available dynamic properties.
    /// You can create custom property manager by inherite this class.
    /// All properties with PropertyAttribute (or inherited) will be registered as available properties after call InitializeProps. <see cref="SensorsPropertyManager"/>
    /// </summary>
    public class PropertyManagerBase
    {
        private Dictionary<string, Property> _properties = new Dictionary<string, Property>();

        /// <summary>
        /// Register new property. Can be used in runtime.
        /// </summary>
        public void RegisterProperty(Property prop)
        {
            if (_properties.ContainsKey(prop.Name))
                throw new ArgumentException($"Property with name {prop.Name} was registered before");
            _properties.Add(prop.Name, prop);
        }

        /// <summary>
        /// Unregister property. Can be used in runtime.
        /// </summary>
        public void UnRegisterProperty(Property prop)
        {
            if (!_properties.ContainsKey(prop.Name))
                throw new ArgumentException($"Property with name {prop.Name} wasn't registered");
            _properties.Remove(prop.Name);
        }

        /// <summary>
        /// Get all registered properties
        /// </summary>
        public IEnumerable<Property> GetProperties()
        {
            return _properties.Values;
        }

        /// <summary>
        /// Get property by name
        /// </summary>
        public Property GetProperty(string name)
        {
            if (_properties.ContainsKey(name))
                return _properties[name];
            return null;
        }

        /// <summary>
        /// Find all properties with custom Property attribute and register them
        /// </summary>
        protected void InitializeProps()
        {
            GetType().GetProperties()
                .Where(x => x.IsDefined(typeof(Property), true))
                .Select(x => x.GetValue(this) as Property)
                .ToList()
                .ForEach(RegisterProperty);
        }
    }
}
