using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Infrastructure.DTO.SensorValue;
using NLog;

namespace Infrastructure.DTO
{
    public static class SensorsRep
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsRep()
        {
            RegisterSensorType<IEngineSensorValue, EngineIdto>();
            RegisterSensorType<IMileageSensorValue, MileageIdto>();
            RegisterSensorType<ISpeedSensorValue, SpeedIdto>();
        }

        private static Dictionary<Type, Type> _iSensorValDictionary = new Dictionary<Type, Type>();
        private static Dictionary<Type, Type> _dtoDictionary = new Dictionary<Type, Type>();
        private static Dictionary<Type, Guid> _guids = new Dictionary<Type, Guid>();

        /// <summary>
        /// Register new sensor contract and dto relation
        /// </summary>
        /// <typeparam name="TSensorValueContract">Type of SensorValue contract</typeparam>
        /// <typeparam name="TDTO">Type of data transform object</typeparam>
        public static void RegisterSensorType<TSensorValueContract, TDTO>(Guid? guid = null)
            where TSensorValueContract : ISensorValue
            where TDTO : IDTO<TSensorValueContract>
        {
            Guid fGuid = guid ?? Guid.NewGuid();
            _iSensorValDictionary.Add(typeof(TSensorValueContract), typeof(TDTO));
            _dtoDictionary.Add(typeof(TDTO), typeof(TSensorValueContract));
            _guids.Add(typeof(TSensorValueContract), fGuid);
        }

        public static IEnumerable<Type> GetSensorValContractTypes()
        {
            return _iSensorValDictionary.Values.AsEnumerable();
        }

        public static Guid GetGuid<TSensorValueContract>()
        {
            if (!_guids.ContainsKey(typeof(TSensorValueContract)))
            {
                KeyNotFoundException e = new KeyNotFoundException(String.Format("Sensor value contract type ({0}) wasn't registered", typeof(TSensorValueContract)));
                logger.Error(e);
                throw e;
            }
            return _guids[typeof(TSensorValueContract)];
        }

        /// <summary>
        /// Map ISensorValue to DTO
        /// </summary>
        public static IDTO<TSensorValueContract> MapToDTO<TSensorValueContract>(TSensorValueContract model)
            where TSensorValueContract : ISensorValue
        {
            if (!_dtoDictionary.ContainsKey(typeof(TSensorValueContract)))
            {
                KeyNotFoundException e = new KeyNotFoundException(String.Format("Sensor value contract type ({0}) wasn't registered", typeof(TSensorValueContract)));
                logger.Error(e);
                throw e;
            }
            var dto = (IDTO<TSensorValueContract>)Activator.CreateInstance(_dtoDictionary[typeof(TSensorValueContract)]);
            dto.MapFromModel(model);
            return dto;
        }
    }
}
