using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Communication.Proxy;
using Infrastructure.Model;

namespace Client.ViewModels
{
    internal class AppViewModel : ViewModelBase
    {
        private ObservableCollection<TerminalViewModel> _terminalViewModels;
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
            TerminalServiceProxy _proxy = new TerminalServiceProxy();
            TerminalViewModels = new ObservableCollection<TerminalViewModel>();
            TerminalViewModels.Clear();
            foreach (var z in _proxy.GetCurStatus())
            {
                TerminalViewModels.Add(new TerminalViewModel(z));
            }
        }
    }
}
