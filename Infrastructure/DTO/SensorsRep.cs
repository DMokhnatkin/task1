using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Contract.Model;
using Infrastructure.DTO.SensorValue;
using NLog;

namespace Infrastructure.DTO
{
    public static class SensorsRep
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsRep()
        {
            RegisterSensorType<EngineSensorValueDTO>(new Guid("ca761232ed4211cebacd00aa0057b223"));
            RegisterSensorType<MileageSensorValueDTO>(new Guid("ca761232ed4211cebacd00aa0057b224"));
            RegisterSensorType<SpeedSensorValueDTO>(new Guid("ca761232ed4211cebacd00aa0057b225"));
        }

        private static Dictionary<Guid, Type> _sensorValTypes = new Dictionary<Guid, Type>();
        private static Dictionary<Type, Guid> _sensorValGuids = new Dictionary<Type, Guid>();

        /// <summary>
        /// Register new sensor
        /// </summary>
        /// <typeparam name="TSensorValue">Type of values which will read sensor</typeparam>
        /// <param name="guid">Guid of sensor (must be unique for all sensors)</param>
        public static void RegisterSensorType<TSensorValue>(Guid? guid = null) where TSensorValue : ISensorValue
        {
            Guid fGuid = guid ?? Guid.NewGuid();
            _sensorValTypes.Add(fGuid, typeof(TSensorValue));
            _sensorValGuids.Add(typeof(TSensorValue), fGuid);
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
    }
}
