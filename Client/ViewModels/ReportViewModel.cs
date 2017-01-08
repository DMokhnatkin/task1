using System;
using System.Collections.Generic;
using Infrastructure.Model.DynamicProperties;
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

        public IEnumerable<KeyValuePair<Property, object>> ReportValues => _report.Values;
    }
}
