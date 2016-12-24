using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Contract.Model;
using Infrastructure.Model.SensorValue;
using NLog;

namespace Infrastructure.Model
{
    public static class SensorsRep
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsRep()
        {
            RegisterSensorType<EngineSensorValue>(new SensorTypeInfo("Engine sensor", ""), new Guid("ca761232ed4211cebacd00aa0057b223"));
            RegisterSensorType<MileageSensorValue>(new SensorTypeInfo("Mileage sensor", "km"), new Guid("ca761232ed4211cebacd00aa0057b224"));
            RegisterSensorType<SpeedSensorValue>(new SensorTypeInfo("Speed sensor", "kmh"), new Guid("ca761232ed4211cebacd00aa0057b225"));
        }

        private static Dictionary<Guid, Type> _sensorValTypes = new Dictionary<Guid, Type>();
        private static Dictionary<Type, Guid> _sensorValGuids = new Dictionary<Type, Guid>();

        private static Dictionary<Guid, SensorTypeInfo> _infos = new Dictionary<Guid, SensorTypeInfo>();

        /// <summary>
        /// Register new sensor
        /// </summary>
        /// <typeparam name="TSensorValue">Type of values which will read sensor</typeparam>
        /// <param name="guid">Guid of sensor (must be unique for all sensors)</param>
        public static void RegisterSensorType<TSensorValue>(SensorTypeInfo info, Guid? guid = null) where TSensorValue : ISensorValue
        {
            Guid fGuid = guid ?? Guid.NewGuid();
            _sensorValTypes.Add(fGuid, typeof(TSensorValue));
            _sensorValGuids.Add(typeof(TSensorValue), fGuid);
            _infos.Add(fGuid, info);
        }

        public static IEnumerable<Type> GetSensorValTypes()
        {
            return _sensorValTypes.Values.AsEnumerable();
        }

        public static Guid GetGuid<TSensorValue>() where TSensorValue : Contract.Model.ISensorValue
        {
            try
            {
                return _sensorValGuids[typeof(TSensorValue)];
            }
            catch (KeyNotFoundException e)
            {
                var ex = new ArgumentException(String.Format("{0} sensor type wasn't registered.", typeof(TSensorValue)));
                logger.Error(ex);
                throw ex;
            }
        }

        public static Guid GetGuid(Type sensorValueType)
        {
            try
            {
                return _sensorValGuids[sensorValueType];
            }
            catch (KeyNotFoundException e)
            {
                var ex = new ArgumentException(String.Format("{0} sensor type wasn't registered.", sensorValueType));
                logger.Error(ex);
                throw ex;
            }
        }

        public static Type GetSensorType(Guid guid)
        {
            try
            {
                return _sensorValTypes[guid];
            }
            catch (KeyNotFoundException e)
            {
                var ex = new ArgumentException(String.Format("Sensor type with {0} guid wasn't registered.", guid));
                logger.Error(ex);
                throw ex;
            }
        }

        public static SensorTypeInfo GetSensorTypeInfo(Guid guid)
        {
            try
            {
                return _infos[guid];
            }
            catch (KeyNotFoundException e)
            {
                var ex = new ArgumentException(String.Format("Sensor type with {0} guid wasn't registered.", guid));
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
