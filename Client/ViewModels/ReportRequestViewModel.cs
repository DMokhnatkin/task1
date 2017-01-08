using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Client.Views;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;
using Infrastructure.Model.Reports;
using Microsoft.Practices.Unity;

namespace Client.ViewModels
{
    internal class ReportPropertySetting
    {
        private ReportProperty _property;

        public string PropertyName => _property.Name;

        public bool Enabled { get; set; } = true;

        public ReportPropertySetting(ReportProperty prop)
        {
            _property = prop;
        }
    }

    internal class ReportRequestViewModel : ViewModelBase
    {
        private IReportService _reportsService = (IReportService)MyUnityContainer.Instance.Resolve<IReportService>();

        private DateTime _from = DateTime.Now - new TimeSpan(1, 0, 0, 0);
        private DateTime _to = DateTime.Now;

        public List<ReportPropertySetting> EnabledReportProperties { get; private set; }

        public DelegateCommand MakeAReportCommand { get; set; }

        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged(nameof(To));
            }
        }

        public ReportRequestViewModel()
        {
            MakeAReportCommand = new DelegateCommand(MakeAReportExecute);
            EnabledReportProperties = 
                DynamicPropertyManagers.Reports.GetProperties()
                    .Select(x => new ReportPropertySetting(x))
                    .ToList();
        }

        private void MakeAReportExecute(object o)
        {
            var res = _reportsService.BuildReport(new ReportSettingsDto(new ReportSettings())).ToBo();

            Window wnd = new Window();
            wnd.WindowStyle = WindowStyle.ToolWindow;
            ReportView z = new ReportView();
            z.DataContext = new ReportViewModel(res);
            wnd.Height = 200;
            wnd.Width = 300;
            wnd.Content = z;
            wnd.ShowDialog();
        }
    }
}
