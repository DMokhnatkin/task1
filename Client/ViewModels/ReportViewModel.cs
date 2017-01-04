using System;

namespace Client.ViewModels
{
    internal class ReportViewModel : ViewModelBase
    {
        private DateTime _from = DateTime.Now - new TimeSpan(1, 0, 0, 0);
        private DateTime _to = DateTime.Now;

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

        public ReportViewModel()
        {
            MakeAReportCommand = new DelegateCommand(MakeAReportExecute);
        }

        private void MakeAReportExecute(object o)
        {
            // Not implemented
        }
    }
}
