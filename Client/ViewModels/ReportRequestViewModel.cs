using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Client.Views;
using Common.Communication.ProxyWrappers;
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

        public ReportProperty ReportProperty {
            get { return _property; }
        }

        public string PropertyName => _property.Name;

        public bool Enabled { get; set; } = true;

        public ReportPropertySetting(ReportProperty prop)
        {
            _property = prop;
        }
    }

    internal class ReportRequestViewModel : ViewModelBase
    {
        private ReportServiceProxyWrapper _reportsService = (ReportServiceProxyWrapper)MyUnityContainer.Instance.Resolve<IReportService>();

        private DateTime _from = DateTime.Now - new TimeSpan(1, 0, 0, 0);
        private DateTime _to = DateTime.Now;
        private string _selectedTerminal = null;

        public List<ReportPropertySetting> EnabledReportProperties { get; private set; }

        public DelegateCommand MakeAReportCommand { get; set; }

        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value > _to ? _to : value;
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

        public string SelectedTerminal
        {
            get { return _selectedTerminal; }
            set
            {
                _selectedTerminal = value;
                RaisePropertyChanged(nameof(SelectedTerminal));
            }
        }

        public ReportRequestViewModel()
        {
            MakeAReportCommand = new DelegateCommand(MakeAReportExecute, false);
            EnabledReportProperties = 
                DynamicPropertyManagers.Reports.GetProperties()
                    .Select(x => new ReportPropertySetting(x))
                    .ToList();
            PropertyChanged += OnPropertyChanged;
            _reportsService.Fault += ReportsServiceOnFault;
            _reportsService.Connected += ReportsServiceOnConnected;
        }

        ~ReportRequestViewModel()
        {
            PropertyChanged -= OnPropertyChanged;
            _reportsService.Fault -= ReportsServiceOnFault;
            _reportsService.Connected -= ReportsServiceOnConnected;
        }

        private void ReportsServiceOnConnected()
        {
            Application.Current.Dispatcher.Invoke(() => MakeAReportCommand.SetCanExecute(true));
        }

        private void ReportsServiceOnFault()
        {
            Application.Current.Dispatcher.Invoke(() => MakeAReportCommand.SetCanExecute(false));
            _reportsService.StartPing();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(SelectedTerminal))
            {
                MakeAReportCommand.SetCanExecute(SelectedTerminal != null);
            }
        }

        private void MakeAReportExecute(object o)
        {
            ReportSettings repSettings = new ReportSettings();
            repSettings.TerminalId = SelectedTerminal;
            repSettings.StartDateTime = From;
            repSettings.EndDateTime = To;
            foreach (var propertySetting in EnabledReportProperties)
            {
                if (propertySetting.Enabled)
                    repSettings.Properties.Add(propertySetting.ReportProperty);
            }

            var res = ReportDto.Unwrap(_reportsService.BuildReport(ReportSettingsDto.Wrap(repSettings)));

            if (res != null)
            {
                Window wnd = new Window();
                wnd.WindowStyle = WindowStyle.ToolWindow;
                ReportView z = new ReportView();
                z.DataContext = new ReportViewModel(res);
                wnd.SizeToContent = SizeToContent.WidthAndHeight;
                wnd.Content = z;
                wnd.ShowDialog();
            }
        }
    }
}
