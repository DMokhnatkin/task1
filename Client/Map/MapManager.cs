using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Media;
using Client.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace Client.Map
{
    internal class MapManager
    {
        private Color _selectedColor = Colors.Red;
        private Color _defaultColor = Colors.Coral;

        private AppViewModel _appViewModel;
        private Microsoft.Maps.MapControl.WPF.Map _map;
        private Dictionary<TerminalViewModel, Pushpin> _pushpins;
        private TerminalViewModel _prevSelectedTerminal;

        internal MapManager(AppViewModel appViewModel, Microsoft.Maps.MapControl.WPF.Map map)
        {
            _pushpins = new Dictionary<TerminalViewModel, Pushpin>();
            _appViewModel = appViewModel;
            _map = map;
            _appViewModel.TerminalViewModels.CollectionChanged += TerminalsOnCollectionChanged;
            _appViewModel.PropertyChanged += _appViewModel_PropertyChanged;
        }

        private void _appViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_appViewModel.TerminalViewModels))
            {
                RedrawPushpins();
            }
            if (e.PropertyName == nameof(_appViewModel.SelectedTerminal))
            {
                UnSelect();
                Select(_appViewModel.SelectedTerminal);
            }
        }

        private void TerminalsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            // Can be optimized by redraw only added/removed items
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                RedrawPushpins();
            }
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                RedrawPushpins();
            }
        }

        /// <summary>
        /// Redraw all pushpins
        /// </summary>
        private void RedrawPushpins()
        {
            _map.Children.Clear();
            foreach (var terminalViewModel in _appViewModel.TerminalViewModels)
            {
                var ps = new Pushpin()
                {
                    Location = new Location(
                        terminalViewModel.Latitude,
                        terminalViewModel.Longitude),
                    Background = new SolidColorBrush(_defaultColor)
                };
                _pushpins[terminalViewModel] = ps;
                _map.Children.Add(ps);
            }
        }

        /// <summary>
        /// Mark some viewmodel pushpin 
        /// </summary>
        /// <param name="_viewModel"></param>
        private void Select(TerminalViewModel _viewModel)
        {
            _pushpins[_viewModel].Background = new SolidColorBrush(_selectedColor);
            _prevSelectedTerminal = _viewModel;
            _map.Center = _pushpins[_viewModel].Location; // Focus on pushpin
        }

        /// <summary>
        /// Unmark cur selected pushpin
        /// </summary>
        private void UnSelect()
        {
            if (_prevSelectedTerminal != null)
                _pushpins[_prevSelectedTerminal].Background = new SolidColorBrush(_defaultColor);
        }
    }
}
