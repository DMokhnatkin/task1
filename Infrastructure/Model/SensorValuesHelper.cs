using Infrastructure.Contract.Model;

namespace Infrastructure.Model
{
    public static class SensorValuesHelper
    {
        public static void AddSensorValue<TSensorValue>(IMetering metering, TSensorValue value)
            where TSensorValue : ISensorValue
        {
            metering.SensorValues[SensorsRep.GetGuid<TSensorValue>()] = value;
        }

        public static TSensorValue GetSensorValue<TSensorValue>(IMetering metering)
            where TSensorValue : ISensorValue
        {
            return (TSensorValue)metering.SensorValues[SensorsRep.GetGuid<TSensorValue>()];
        }
    }
}
