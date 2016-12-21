using System;
using Infrastructure.Model;

namespace Client.ViewModels
{
    internal class TerminalViewModel : ViewModelBase
    {
        private TerminalStatus _terminalStatus;

        public void UpdateTerminalStatus(TerminalStatus newTerminalStatus)
        {
            _terminalStatus = newTerminalStatus;
            // Raise all properties changed
            RaisePropertyChanged(null);
        }

        public TerminalViewModel(TerminalStatus terminalStatus)
        {
            UpdateTerminalStatus(terminalStatus);
        }

        public string Id => _terminalStatus.TerminalId;

        public DateTime LastActiveDateTime => _terminalStatus.LastMetering.Time;

        public float Longitude => _terminalStatus.LastMetering.Longitude;

        public float Latitude => _terminalStatus.LastMetering.Latitude;
    }
}
