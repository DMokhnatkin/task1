
using System;

namespace Infrastructure.Model.DynamicProperties.Specialized.Properties
{
    public class ReportProperty : Property
    {
        public string Unit { get; set; }

        /// <inheritdoc />
        public ReportProperty(string name, Type typeOfValue, string unit = "") : base(name, typeOfValue)
        {
            Unit = unit;
        }
    }
}
