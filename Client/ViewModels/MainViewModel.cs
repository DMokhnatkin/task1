using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Common.Communication.ProxyWrappers;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Microsoft.Practices.Unity;

namespace Client.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private const int UpdatePeriod = 2000;

        private readonly DispatcherTimer _loadAllTerminalsStatus;
        readonly TerminalServiceProxyWrapper _proxy = (TerminalServiceProxyWrapper) MyUnityContainer.Instance.Resolve<ITerminalsService>();

        private ObservableCollection<TerminalViewModel> _terminalViewModels =
            new ObservableCollection<TerminalViewModel>();

        private object _terminalViewModelsLock = new object();

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

        public MainViewModel()
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
                // Start ping and wait
                _proxy.StartPing();
            };
            _proxy.StartPing();

            _loadAllTerminalsStatus = new DispatcherTimer();
            _loadAllTerminalsStatus.Interval = new TimeSpan(0 ,0, 0, 0, UpdatePeriod);
            _loadAllTerminalsStatus.Tick += (sender, args) =>
            {
                try
                {
                    LoadStatsFromServer();
                }
                catch (Exception)
                {
                    // ignored
                };
            };
            _loadAllTerminalsStatus.Start();
        }

        private async void LoadStatsFromServer()
        {
            // Get from server new collection
            var statsColl = new List<TerminalStatus>();
            await Task.Run(() =>
            {
                statsColl = _proxy.GetCurStatus();
            }).ConfigureAwait(false);

            lock (_terminalViewModelsLock)
            {
                Dictionary<string, int> prevCol;
                prevCol = TerminalViewModels
                    .Select((val, index) => new {val, index})
                    .ToDictionary(t => t.val.Id, z => z.index);
                // Apply new values
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var z in statsColl)
                    {
                        if (prevCol.ContainsKey(z.TerminalId))
                            TerminalViewModels[prevCol[z.TerminalId]].ChangeModel(z);
                        else
                            TerminalViewModels.Add(new TerminalViewModel(z));
                    }
                });
            }
        }
    }
}
