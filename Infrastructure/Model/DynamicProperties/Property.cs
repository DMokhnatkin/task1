using System;

namespace Infrastructure.Model.DynamicProperties
{
    public class Property
    {
        public string Name { get; protected set; }

        public Type TypeOfValue { get; protected set; }

        public Property(string name, Type typeOfValue)
        {
            Name = name;
            TypeOfValue = typeOfValue;
        }
    }
}
