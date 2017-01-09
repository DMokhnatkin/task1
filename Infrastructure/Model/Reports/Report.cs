using Infrastructure.Model.DynamicProperties;

namespace Infrastructure.Model.Reports
{
    public class Report
    {
        public ReportSettings ReportSettings { get; set; } = new ReportSettings();

        public PropertiesCollection Values { get; set; } = new PropertiesCollection();
    }
}
