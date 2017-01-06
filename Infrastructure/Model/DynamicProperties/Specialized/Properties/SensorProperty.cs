using System;

namespace Infrastructure.Model.DynamicProperties.Specialized.Properties
{
    public class SensorProperty : Property
    {
        public string Unit { get; set; }

        public SensorProperty(string name, Type typeOfValue, string unit = "") : base(name, typeOfValue)
        {
            Unit = unit;
        }
    }
}
