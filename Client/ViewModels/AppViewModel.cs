using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Communication.Proxy;
using Infrastructure.Model;

namespace Client.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        private ObservableCollection<TerminalViewModel> _terminalViewModels = new ObservableCollection<TerminalViewModel>();
        public ObservableCollection<TerminalViewModel> TerminalViewModels
        {
            get { return _terminalViewModels; }
            set
            {
                _terminalViewModels = value;
                RaisePropertyChanged(nameof(TerminalViewModels));
            }
        }

        private TerminalViewModel _selectedTerminal;
        public TerminalViewModel SelectedTerminal
        {
            get { return _selectedTerminal; }
            set
            {
                _selectedTerminal = value;
                RaisePropertyChanged(nameof(SelectedTerminal));
            }
        }

        public AppViewModel()
        {
            LoadStatsFromServer();
        }

        private async void LoadStatsFromServer()
        {
            var newCol = new ObservableCollection<TerminalViewModel>();
            TerminalServiceProxy _proxy = new TerminalServiceProxy();
            // TODO: make proxy async
            await Task.Run(() =>
            {
                var stats = _proxy.GetCurStatus();
                foreach (var z in stats)
                {
                    newCol.Add(new TerminalViewModel(z));
                }
            });
            TerminalViewModels = newCol;
        }
    }
}
