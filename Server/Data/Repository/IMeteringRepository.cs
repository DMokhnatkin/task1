using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Server.Data.Repository
{
    internal interface IMeteringRepository
    {
        void SaveMetering(IMetering metering);

        Task<IMetering> GetLastMetering(string terminalId);

        Task<object> GetMaxPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        Task<double> GetAvgPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        Task<double> GetSumPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        Task<double> GetLastFirstDifferencePropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        Task<List<Metering>> GetMeterings(string terminalId, DateTime start, DateTime end, SensorProperty prop);
    }
}
