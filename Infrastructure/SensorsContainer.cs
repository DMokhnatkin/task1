using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;
using NLog;

namespace Infrastructure
{
    public static class SensorsContainer
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static SensorsContainer()
        {
            RegisterSensorType<IEngineSensorValue, EngineDTO>();
            RegisterSensorType<IMileageSensorValue, MileageDTO>();
            RegisterSensorType<ISpeedSensorValue, SpeedDTO>();
        }

        private static Dictionary<Type, Type> _iSensorValDictionary = new Dictionary<Type, Type>();
        private static Dictionary<Type, Type> _dtoDictionary = new Dictionary<Type, Type>();

        /// <summary>
        /// Access to DTO by Model
        /// </summary>
        private static Dictionary<Type, Type> _modelToDtoDictionary = new Dictionary<Type, Type>();

        /// <summary>
        /// Access to Model by Dto
        /// </summary>
        private static Dictionary<Type, Type> _dtoToModelDictionary = new Dictionary<Type, Type>();

        /// <summary>
        /// Register new sensor contract and dto relation
        /// </summary>
        /// <typeparam name="TSensorValueContract">Type of SensorValue contract</typeparam>
        /// <typeparam name="TDTO">Type of data transform object</typeparam>
        public static void RegisterSensorType<TSensorValueContract, TDTO, TSensorValueModel>()
            where TSensorValueContract : ISensorValue
            where TDTO : IDTO<TSensorValueContract>
            where TSensorValueModel : TSensorValueContract
        {
            _iSensorValDictionary.Add(typeof(TSensorValueContract), typeof(TDTO));
            _dtoDictionary.Add(typeof(TDTO), typeof(TSensorValueContract));

            _modelToDtoDictionary.Add(typeof(TSensorValueModel), typeof(TDTO));
            _dtoToModelDictionary.Add(typeof(TDTO), typeof(TSensorValueModel));
        }

        public static IEnumerable<Type> GetSensorValContractTypes()
        {
            return _iSensorValDictionary.Values.AsEnumerable();
        }

        public static IDTO<TSensorValueModel> ModelToDto<TSensorValueModel>(TSensorValueModel model)
        {
            var dto = (IDTO<TSensorValueModel>)Activator.CreateInstance(_modelToDtoDictionary[typeof(TSensorValueModel)]);
            dto.MapFromModel(model);
            return dto;
        }

        public static IDTO<ISensorValue> ModelToDto(ISensorValue model)
        {
            var dto = (IDTO<ISensorValue>)Activator.CreateInstance(_modelToDtoDictionary[model.GetType()]);
            dto.MapFromModel(model);
            return dto;
        }

        public static TSensorValueModel DtoToModel<TSensorValueModel>(IDTO<TSensorValueModel> dto)
        {
            var model = (TSensorValueModel)Activator.CreateInstance(_dtoToModelDictionary[dto.GetType()]);
            model.MapFromModel(model);
            return dmodel;
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

        /// <summary>
        /// Map ISensorValue to DTO
        /// </summary>
        public static IDTO<ISensorValue> MapToDTORuntime(ISensorValue model)
        {
            if (!_dtoDictionary.ContainsKey(model.GetType()))
            {
                KeyNotFoundException e = new KeyNotFoundException(String.Format("Sensor value contract type ({0}) wasn't registered", model.GetType()));
                logger.Error(e);
                throw e;
            }
            var dto = (IDTO<ISensorValue>)Activator.CreateInstance(_dtoDictionary[model.GetType()]);
            dto.MapFromModel(model);
            return dto;
        }
    }
}
