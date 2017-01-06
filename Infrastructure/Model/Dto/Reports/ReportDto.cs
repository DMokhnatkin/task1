using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportDto
    {
        [DataMember]
        public ReportSettingsDto ReportSettings { get; set; } = new ReportSettingsDto();

        [DataMember]
        public Dictionary<string, object> Values = new Dictionary<string, object>();

        public ReportDto(Report report)
        {
            ReportSettings.TerminalId = report.TerminalId;
            ReportSettings.StartTime = report.StartDateTime;
            ReportSettings.EndTime = report.EndDateTime;

            foreach (var z in report.Values)
            {
                Values.Add(z.Key.Name, z.Value);
            }
        }

        public Report ToBo()
        {
            Report res = new Report();
            res.TerminalId = ReportSettings.TerminalId;
            res.StartDateTime = ReportSettings.StartTime;
            res.EndDateTime = ReportSettings.EndTime;

            foreach (var z in Values)
            {
                res.Values.SetValue(
                    DynamicPropertyManagers.Reports.GetProperty(z.Key),
                    z.Value);
            }
            return res;
        }
    }
}
