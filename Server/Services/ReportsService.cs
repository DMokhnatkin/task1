using System;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;
using Microsoft.Practices.Unity;
using Server.Data.Repository;

namespace Server.Services
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single,
        IncludeExceptionDetailInFaults = true)]
    public class ReportsService : IReportService
    {
        private IMeteringRepository _meteringRepository = MyUnityContainer.Instance.Resolve<IMeteringRepository>();

        /// <inheritdoc />
        public ReportDto BuildReport(ReportSettingsDto settings)
        {
            var rep = new Report();
            var repSettings = ReportSettingsDto.Unwrap(settings);
            rep.ReportSettings = repSettings;

            if (repSettings.Properties.Contains(DynamicPropertyManagers.Reports.MaxSpeed))
            {
                var maxSpeed = _meteringRepository.GetMaxPropertyValue(
                    repSettings.TerminalId,
                    DynamicPropertyManagers.Sensors.SpeedKmh,
                    repSettings.StartDateTime,
                    repSettings.EndDateTime);
                rep.Values.SetValue(
                    DynamicPropertyManagers.Reports.MaxSpeed, 
                    maxSpeed);
            }
            if (repSettings.Properties.Contains(DynamicPropertyManagers.Reports.AvgSpeed))
            {
                var avgSpeed = _meteringRepository.GetAvgPropertyValue(
                    repSettings.TerminalId,
                    DynamicPropertyManagers.Sensors.SpeedKmh,
                    repSettings.StartDateTime,
                    repSettings.EndDateTime);
                rep.Values.SetValue(
                    DynamicPropertyManagers.Reports.AvgSpeed,
                    (float)avgSpeed);
            }
            if (repSettings.Properties.Contains(DynamicPropertyManagers.Reports.MileageKm))
            {
                var differencePropertyValue = _meteringRepository.GetLastFirstDifferencePropertyValue(
                    repSettings.TerminalId,
                    DynamicPropertyManagers.Sensors.MileageKm,
                    repSettings.StartDateTime,
                    repSettings.EndDateTime);
                rep.Values.SetValue(
                    DynamicPropertyManagers.Reports.MileageKm,
                    (float)differencePropertyValue);
            }
            if (repSettings.Properties.Contains(DynamicPropertyManagers.Reports.EngineWorkTime))
            {
                var meterings = _meteringRepository.GetMeterings(
                    repSettings.TerminalId,
                    repSettings.StartDateTime,
                    repSettings.EndDateTime,
                    DynamicPropertyManagers.Sensors.IsEngineRunning);
                TimeSpan engineWorkTime = new TimeSpan(0);
                for (int i = 1; i < meterings.Count; i++)
                {
                    if (
                        (bool) meterings[i - 1].SensorValues.GetValue(DynamicPropertyManagers.Sensors.IsEngineRunning) &&
                        (bool) meterings[i].SensorValues.GetValue(DynamicPropertyManagers.Sensors.IsEngineRunning))
                    {
                        engineWorkTime = engineWorkTime.Add(meterings[i].Time - meterings[i - 1].Time);
                    }
                }
                rep.Values.SetValue(
                    DynamicPropertyManagers.Reports.EngineWorkTime,
                    engineWorkTime);
            }

            return ReportDto.Wrap(rep);
        }
    }
}
