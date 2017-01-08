using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Contract;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportDto : IDto<Report>
    {
        [DataMember]
        public ReportSettingsDto ReportSettings { get; set; }

        [DataMember]
        public Dictionary<string, object> Values = new Dictionary<string, object>();

        public ReportDto(Report report)
        {
            ReportSettings = new ReportSettingsDto(report.ReportSettings);

            foreach (var z in report.Values)
            {
                Values.Add(z.Key.Name, z.Value);
            }
        }

        public Report ToBo()
        {
            Report res = new Report();

            res.ReportSettings = ReportSettings.ToBo();
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
