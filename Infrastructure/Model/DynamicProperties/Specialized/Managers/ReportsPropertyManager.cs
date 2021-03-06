﻿using System;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Infrastructure.Model.DynamicProperties.Specialized.Managers
{
    public class ReportsPropertyManager : PropertyManagerBase<ReportProperty>
    {
        [Property]
        public ReportProperty MileageKm { get; } = 
            new ReportProperty("Mileage", typeof(float), "Km");

        [Property]
        public ReportProperty EngineWorkTime { get; } = 
            new ReportProperty("EngineWorkTime", typeof(TimeSpan));

        [Property]
        public ReportProperty MaxSpeed { get; } = 
            new ReportProperty("Max speed", typeof(float), "Kmh");

        [Property]
        public ReportProperty AvgSpeed { get; } =
            new ReportProperty("Average speed", typeof(float), "Kmh");

        internal ReportsPropertyManager()
        {
            InitializeProps();
        }
    }
}
