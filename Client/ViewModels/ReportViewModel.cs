using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;
using Infrastructure.Model.Reports;

namespace Client.ViewModels
{
    internal class ReportViewModel : ViewModelBase
    {
        private Report _report;

        public ReportViewModel(Report report)
        {
            _report = report;
        }

        public string TerminalId => _report.ReportSettings.TerminalId;

        public DateTime StartDateTime => _report.ReportSettings.StartDateTime;

        public DateTime EndDateTime => _report.ReportSettings.EndDateTime;

        public IEnumerable<KeyValuePair<ReportProperty, object>> ReportValues => 
            // Transform all Property keys to ReportProperty keys
            _report.Values.ToDictionary(z => (ReportProperty)z.Key, t => t.Value);
    }
}
