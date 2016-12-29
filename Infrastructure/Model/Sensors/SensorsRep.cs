using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Contract.Model;
using NLog;

namespace Infrastructure.Model.Sensors
{
    public static class SensorsRep
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsRep()
        {
            InitializeSensorTypes();
        }

        /// <summary>
        /// Find all classes (in all assemblies) which has SensorTypeAttribute 
        /// and register them
        /// </summary>
        private static void InitializeSensorTypes()
        {
            var sensorTypeClasses =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                where IsSensorTypeClass(t)
                select t;
            foreach (var sensorTypeClass in sensorTypeClasses)
            {
                RegisterSensorType(sensorTypeClass);
            }
        }

        private static Dictionary<Guid, Type> _guidToType = new Dictionary<Guid, Type>();
        private static Dictionary<Type, Guid> _typeToGuid = new Dictionary<Type, Guid>();
        // Just cache (GetCustomAttribute is heavy operation)
        private static Dictionary<Guid, SensorTypeAttribute> _attributes = new Dictionary<Guid, SensorTypeAttribute>();

        private static bool IsSensorTypeClass(Type type)
        {
            return type.GetInterface(nameof(ISensorValue)) != null &&
                   type.IsDefined(typeof(SensorTypeAttribute), false);
        }

        private static void RegisterSensorType(Type sensorType)
        {
            if (!IsSensorTypeClass(sensorType))
            {
                ArgumentException e = new ArgumentException(
                    $@"{sensorType} isn't sensor type class. 
                       Sensor type class must has {typeof(SensorTypeAttribute)} attribute and
                       implements {typeof(ISensorValue)}");
                logger.Error(e);
                throw e;
            }
            var attr = (SensorTypeAttribute) Attribute.GetCustomAttribute(sensorType, typeof(SensorTypeAttribute));
            _guidToType.Add(attr.Guid, sensorType);
            _typeToGuid.Add(sensorType, attr.Guid);
            _attributes.Add(attr.Guid, attr);
        }

        /// <summary>
        /// Get SensorTypeAttribute instance for givven sensor type.
        /// This value will be getted from attribute.
        /// </summary>
        /// <param name="guid">Guid of sensor type</param>
        public static SensorTypeAttribute GetSensorTypeAttribute(Guid guid)
        {
            try
            {
                return _attributes[guid];
            }
            catch (Exception)
            {
                ArgumentException e = new ArgumentException(
                    $"Sensor type with {guid} guid wasn't registered");
                logger.Error(e);
                throw e;
            }
        }

        public static SensorTypeAttribute GetSensorTypeAttribute(Type sensorType)
        {
            try
            {
                Guid guid = _typeToGuid[sensorType];
                return GetSensorTypeAttribute(guid);
            }
            catch (Exception)
            {
                ArgumentException e = new ArgumentException(
                    $"Sensor type {sensorType} wasn't registered");
                logger.Error(e);
                throw e;
            }
        }

        public static Type GetSensorType(Guid guid)
        {
            try
            {
                return _guidToType[guid];
            }
            catch (Exception)
            {
                ArgumentException e = new ArgumentException(
                    $"Sensor type with {guid} guid wasn't registered");
                logger.Error(e);
                throw e;
            }
        }

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider = null)
        {
            return _guidToType.Values.AsEnumerable();
        }
    }
}
