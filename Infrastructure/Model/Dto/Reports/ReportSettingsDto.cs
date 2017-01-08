using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Infrastructure.Contract;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportSettingsDto : IDto<ReportSettings>
    {
        [DataMember]
        public string TerminalId { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public List<string> Properties { get; set; }

        public ReportSettingsDto(ReportSettings sett)
        {
            TerminalId = sett.TerminalId;
            StartTime = sett.StartDateTime;
            EndTime = sett.EndDateTime;
            Properties = sett.Properties.Select(x => x.Name).ToList();
        }

        /// <inheritdoc />
        public ReportSettings ToBo()
        {
            var res = new ReportSettings();
            res.TerminalId = TerminalId;
            res.StartDateTime = StartTime;
            res.EndDateTime = EndTime;

            foreach (var z in Properties)
            {
                res.Properties.Add(DynamicPropertyManagers.Reports.GetProperty(z));
            }

            return res;
        }
    }
}
