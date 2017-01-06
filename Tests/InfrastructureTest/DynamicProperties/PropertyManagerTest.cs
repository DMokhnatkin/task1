
using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest.DynamicProperties
{
    [TestClass]
    public class PropertyManagerTest
    {
        [TestMethod]
        public void TestRuntimePropRegister()
        {
            DynamicPropertyManagers.Sensors.RegisterProperty(new Property("test", typeof(bool)));
            var z = DynamicPropertyManagers.Sensors.GetProperty("test");
            Assert.IsNotNull(z);
            
            PropertiesCollection coll = new PropertiesCollection();
            coll.SetValue(z, true);
            Assert.AreEqual(true, coll.GetValue(z));
        }
    }
}
