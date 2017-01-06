using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.DynamicProperties.Specialized.Managers;

namespace Infrastructure.Model.DynamicProperties
{
    /// <summary>
    /// Property manager stores available dynamic properties.
    /// You can create custom property manager by inherite this class.
    /// All properties with TPropertyAttribute (or inherited) will be registered as available properties after call InitializeProps. <see cref="SensorsPropertyManager"/>
    /// </summary>
    public class PropertyManagerBase<TProperty>
        where TProperty : Property
    {
        private Dictionary<string, TProperty> _properties = new Dictionary<string, TProperty>();

        /// <summary>
        /// Register new property. Can be used in runtime.
        /// </summary>
        public void RegisterProperty(TProperty prop)
        {
            if (_properties.ContainsKey(prop.Name))
                throw new ArgumentException($"Property with name {prop.Name} was registered before");
            _properties.Add(prop.Name, prop);
        }

        /// <summary>
        /// Unregister property. Can be used in runtime.
        /// </summary>
        public void UnRegisterProperty(TProperty prop)
        {
            if (!_properties.ContainsKey(prop.Name))
                throw new ArgumentException($"Property with name {prop.Name} wasn't registered");
            _properties.Remove(prop.Name);
        }

        public bool IsPropertyRegistered(TProperty prop)
        {
            return _properties.ContainsKey(prop.Name);
        }

        /// <summary>
        /// Get all registered properties
        /// </summary>
        public IEnumerable<TProperty> GetProperties()
        {
            return _properties.Values;
        }

        /// <summary>
        /// Get property by name
        /// </summary>
        public TProperty GetProperty(string name)
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
                .Where(x => x.IsDefined(typeof(PropertyAttribute), true))
                .Where(x => x.GetValue(this) is TProperty)
                .Select(x => x.GetValue(this) as TProperty)
                .ToList()
                .ForEach(RegisterProperty);
        }
    }
}
