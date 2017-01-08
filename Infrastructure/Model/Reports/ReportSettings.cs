﻿
using System;
using System.Collections.Generic;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Infrastructure.Model.Reports
{
    public class ReportSettings
    {
        public string TerminalId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public HashSet<ReportProperty> Properties { get; set; } 
            = new HashSet<ReportProperty>();
    }
}
