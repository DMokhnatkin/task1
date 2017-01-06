using System;
using Infrastructure.Model.DynamicProperties;

namespace Infrastructure.Model.Reports
{
    public class Report
    {
        public string TerminalId { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public PropertiesCollection Values { get; set; }
    }
}
