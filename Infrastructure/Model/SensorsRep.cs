using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Contract.Model;
using Infrastructure.Model.Sensors;
using NLog;

namespace Infrastructure.Model
{
    public static class SensorsRep
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsRep()
        {
            RegisterSensorType<EngineSensor>(Guid.NewGuid());
            RegisterSensorType<MileageSensor>(Guid.NewGuid());
            RegisterSensorType<SpeedSensor>(Guid.NewGuid());
        }

        private static Dictionary<Guid, Type> _sensorTypes = new Dictionary<Guid, Type>();
        private static Dictionary<Type, Guid> _sensorGuids = new Dictionary<Type, Guid>();

        public static void RegisterSensorType<TSensor>(Guid guid) where TSensor : ISensorValue
        {
            _sensorTypes.Add(guid, typeof(TSensor));
            _sensorGuids.Add(typeof(TSensor), guid);
        }

        public static IEnumerable<Type> GetSensorTypes()
        {
            return _sensorTypes.Values.AsEnumerable();
        }

        public static Guid GetGuid<TSensor>() where TSensor : ISensorValue
        {
            try
            {
                return _sensorGuids[typeof(TSensor)];
            }
            catch (KeyNotFoundException e)
            {
                var ex = new ArgumentException(String.Format("{0} sensor type wasn't registered.", typeof(TSensor)));
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
