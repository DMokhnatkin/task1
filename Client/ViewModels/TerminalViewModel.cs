using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Model.Dto;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;

namespace Client.ViewModels
{
    internal class TerminalViewModel : ViewModelBase
    {
        private TerminalStatusDto _terminalStatusDto;

        public void UpdateTerminalStatus(TerminalStatusDto newTerminalStatusDto)
        {
            ChangeModel(newTerminalStatusDto);
        }

        public void ChangeModel(TerminalStatusDto newModel)
        {
            _terminalStatusDto = newModel;
            // Raise all properties changed
            RaisePropertyChanged(null);
        }

        public TerminalViewModel(TerminalStatusDto terminalStatusDto)
        {
            UpdateTerminalStatus(terminalStatusDto);
        }

        public string Id => _terminalStatusDto.TerminalId;

        public DateTime LastActiveDateTime => _terminalStatusDto.LastMetering.Time;

        public float Longitude => _terminalStatusDto.LastMetering.Longitude;

        public float Latitude => _terminalStatusDto.LastMetering.Latitude;

        public List<SensorValueViewModel> SensorValues => 
            _terminalStatusDto.LastMetering.ToBo().SensorValues.Select(x => new SensorValueViewModel(x.Key as SensorProperty, _terminalStatusDto.LastMetering.ToBo().SensorValues)).ToList();
    }
}
