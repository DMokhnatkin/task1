using System;
using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest.DynamicProperties
{
    [TestClass]
    public class PropertiesCollectionTest
    {
        [TestMethod]
        public void TestSensorsProp()
        {
            PropertiesCollection coll = new PropertiesCollection();
            coll.SetValue(DynamicPropertyManagers.Sensors.MileageKm, 3.4f);
            coll.SetValue(DynamicPropertyManagers.Sensors.SpeedKmh, 3.2f);
            coll.SetValue(DynamicPropertyManagers.Sensors.IsEngineRunning, true);

            Assert.AreEqual(3.2f, coll.GetValue(DynamicPropertyManagers.Sensors.SpeedKmh));
            Assert.AreEqual(3.4f, coll.GetValue(DynamicPropertyManagers.Sensors.MileageKm));
            Assert.AreEqual(true, coll.GetValue(DynamicPropertyManagers.Sensors.IsEngineRunning));
        }
    }
}
