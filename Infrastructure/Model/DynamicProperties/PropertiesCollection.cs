using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Infrastructure.Model.DynamicProperties
{
    /// <summary>
    /// Instance of this class can store dynamic property values
    /// </summary>
    public class PropertiesCollection : IEnumerable<KeyValuePair<Property, object>>
    {
        Dictionary<Property, object> _propValues = new Dictionary<Property, object>();

        public object GetValue(Property prop)
        {
            if (_propValues.ContainsKey(prop))
                return _propValues[prop];
            return null;
        }

        public void SetValue(Property prop, object val)
        {
            if (!prop.TypeOfValue.IsInstanceOfType(val))
            {
                ArgumentException a = new ArgumentException("Invalid property value type");
                throw a;
            }
            _propValues[prop] = val;
        }

        public IEnumerator<KeyValuePair<Property, object>> GetEnumerator()
        {
            return _propValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _propValues.GetEnumerator();
        }
    }
}
