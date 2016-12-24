using Infrastructure.Contract.Model;
using Infrastructure.Model.Sensors;

namespace Infrastructure.Model
{
    public static class SensorValuesHelper
    {
        public static void AddSensorValue<TSensorValue>(IMetering metering, TSensorValue value)
            where TSensorValue : ISensorValue
        {
            metering.SensorValues[SensorsRep.GetSensorTypeAttribute(typeof(TSensorValue)).Guid] = value;
        }

        public static TSensorValue GetSensorValue<TSensorValue>(IMetering metering)
            where TSensorValue : ISensorValue
        {
            return (TSensorValue)metering.SensorValues[SensorsRep.GetSensorTypeAttribute(typeof(TSensorValue)).Guid];
        }
    }
}
