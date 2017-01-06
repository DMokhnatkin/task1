using System;
using Infrastructure.Model.DynamicProperties.Specialized.Managers;

namespace Infrastructure.Model.DynamicProperties.Specialized
{
    /// <summary>
    /// Built in property managers
    /// </summary>
    public static class DynamicPropertyManagers
    {
        static Lazy<ReportsPropertyManager> _reportsPropManager = 
            new Lazy<ReportsPropertyManager>(() => new ReportsPropertyManager());
        static Lazy<SensorsPropertyManager> _sensorsPropManager = 
            new Lazy<SensorsPropertyManager>(() => new SensorsPropertyManager());

        public static SensorsPropertyManager Sensors => _sensorsPropManager.Value;

        public static ReportsPropertyManager Reports => _reportsPropManager.Value;
    }
}
