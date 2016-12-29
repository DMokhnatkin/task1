using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common.Communication.Proxy;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Microsoft.Practices.Unity;

namespace Client.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        private const int sleepTime = 1000;

        TerminalServiceProxy _proxy = (TerminalServiceProxy) MyUnityContainer.Instance.Resolve<ITerminalsService>();

        private ObservableCollection<TerminalViewModel> _terminalViewModels =
            new ObservableCollection<TerminalViewModel>();

        private TerminalViewModel _selectedTerminal;
        private bool _isServerReady = false;

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

        public bool IsServerReady
        {
            get { return _isServerReady; }
            set
            {
                _isServerReady = value;
                RaisePropertyChanged(nameof(IsServerReady));
            }
        }

        public AppViewModel()
        {
            WaitServer(OnServerReady);
        }

        // Call callbackAction when proxy is ready
        private async void WaitServer(Action callback)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var z = _proxy.IsAlive();
                        if (z)
                        {
                            // Invoke callback in main thread (we need to change ui)
                            Dispatcher.CurrentDispatcher.Invoke(callback);
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    Thread.Sleep(sleepTime);
                 }
             });
        }

        private void OnServerReady()
        {
            IsServerReady = true;
            LoadStatsFromServer();
        }

        private async void LoadStatsFromServer()
        {
            var newCol = new ObservableCollection<TerminalViewModel>();
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
