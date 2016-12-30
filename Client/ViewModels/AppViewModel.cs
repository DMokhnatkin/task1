using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Common.Communication.Proxy;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Microsoft.Practices.Unity;

namespace Client.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        TerminalServiceProxy _proxy = (TerminalServiceProxy) MyUnityContainer.Instance.Resolve<ITerminalsService>();

        private ObservableCollection<TerminalViewModel> _terminalViewModels =
            new ObservableCollection<TerminalViewModel>();

        private TerminalViewModel _selectedTerminal;
        private bool _isServerConnected = false;

        public ObservableCollection<TerminalViewModel> TerminalViewModels
        {
            get { return _terminalViewModels; }
            set
            {
                _terminalViewModels = value;
                RaisePropertyChanged(nameof(TerminalViewModels));
            }
        }

        public TerminalViewModel SelectedTerminal
        {
            get { return _selectedTerminal; }
            set
            {
                _selectedTerminal = value;
                RaisePropertyChanged(nameof(SelectedTerminal));
            }
        }

        public bool IsServerConnected
        {
            get { return _isServerConnected; }
            set
            {
                _isServerConnected = value;
                RaisePropertyChanged(nameof(IsServerConnected));
            }
        }

        public AppViewModel()
        {
            _proxy.Connected += () =>
            {
                IsServerConnected = true;
                // Invoke LoadStatsFromServer in main thread (we need to change ui)
                Application.Current.Dispatcher.Invoke(LoadStatsFromServer);
            };
            _proxy.Fault += () =>
            {
                IsServerConnected = false;
                // Try repair connection
                _proxy.StartPing();
            };
            _proxy.StartPing();
        }

        private async void LoadStatsFromServer()
        {
            var newCol = new ObservableCollection<TerminalViewModel>();
            await Task.Run(() =>
            {
                var stats = _proxy.GetCurStatus();
                foreach (var z in stats)
                {
                    newCol.Add(new TerminalViewModel(z));
                }
            }).ConfigureAwait(false);
            Application.Current.Dispatcher.Invoke(() => TerminalViewModels = newCol);
        }
    }
}
