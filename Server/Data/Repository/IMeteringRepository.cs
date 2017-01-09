using System;
using System.Collections.Generic;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Server.Data.Repository
{
    internal interface IMeteringRepository
    {
        void SaveMetering(IMetering metering);

        IMetering GetLastMetering(string terminalId);

        object GetMaxPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        double GetAvgPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        double GetSumPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        double GetLastFirstDifferencePropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end);

        List<Metering> GetMeterings(string terminalId, DateTime start, DateTime end, SensorProperty prop);
    }
}
