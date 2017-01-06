using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportSettingsDto
    {
        [DataMember]
        public string TerminalId { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public HashSet<ReportProperty> Properties { get; set; } = new HashSet<ReportProperty>();
    }
}
