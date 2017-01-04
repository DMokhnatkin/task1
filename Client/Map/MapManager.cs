using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using Client.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace Client.Map
{
    internal class MapManager
    {
        private Color _selectedColor = Colors.Red;
        private Color _defaultColor = Colors.Coral;

        private MainViewModel _appViewModel;
        private Microsoft.Maps.MapControl.WPF.Map _map;
        private Dictionary<TerminalViewModel, Pushpin> _pushpins;
        private TerminalViewModel _prevSelectedTerminal;

        internal MapManager(MainViewModel appViewModel, Microsoft.Maps.MapControl.WPF.Map map)
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
                RedrawPushpinsUI();
            }
            if (e.PropertyName == nameof(_appViewModel.SelectedTerminal))
            {
                UnSelect();
                if (_appViewModel.SelectedTerminal != null)
                    Select(_appViewModel.SelectedTerminal);
            }
        }

        private void TerminalsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var z in args.NewItems.OfType<TerminalViewModel>())
                {
                    RedrawPushpinUI(z);
                }
            }
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var z in args.NewItems.OfType<TerminalViewModel>())
                {
                    RemovePushpinUI(z);
                }
            }
        }

        #region Pushpins UI

        /// <summary>
        /// Create and draw pushpin for specifed terminal
        /// </summary>
        private Pushpin DrawPushpinUI(TerminalViewModel terminalViewModel)
        {
            var ps = new Pushpin()
            {
                Location = new Location(
                        terminalViewModel.Latitude,
                        terminalViewModel.Longitude),
                Background = new SolidColorBrush(_defaultColor)
            };
            // If terminal view model is changed, redraw its pushpin
            terminalViewModel.PropertyChanged += TerminalViewModelOnPropertyChanged;
            _pushpins[terminalViewModel] = ps;
            _map.Children.Add(ps);
            return ps;
        }

        // If terminal view model is changed, redraw its pushpin
        private void TerminalViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var z = sender as TerminalViewModel;
            if (z != null)
                RedrawPushpinUI(z);
        }

        private void RemovePushpinUI(TerminalViewModel terminalViewModel)
        {
            if (_pushpins.ContainsKey(terminalViewModel))
            {
                terminalViewModel.PropertyChanged -= TerminalViewModelOnPropertyChanged;
                _map.Children.Remove(_pushpins[terminalViewModel]);
                _pushpins.Remove(terminalViewModel);
            }
        }

        /// <summary>
        /// Remove all pushpins and draw them again.
        /// </summary>
        private void RedrawPushpinsUI()
        {
            _map.Children.Clear();
            foreach (var terminalViewModel in _appViewModel.TerminalViewModels)
            {
                DrawPushpinUI(terminalViewModel);
            }
        }

        /// <summary>
        /// If pushpin for specifed terminal is exists redraw it.
        /// Otherwise create and draw new pushpin.
        /// </summary>
        private void RedrawPushpinUI(TerminalViewModel terminalViewModel)
        {
            if (_pushpins.ContainsKey(terminalViewModel))
            {
                UpdatePushpinUIPos(_pushpins[terminalViewModel], terminalViewModel);
            }
            else
            {
                DrawPushpinUI(terminalViewModel);
            }
        }

        /// <summary>
        /// Change pushpin location
        /// </summary>
        private void UpdatePushpinUIPos(Pushpin pushpin, TerminalViewModel terminalViewModel)
        {
            pushpin.Location = new Location(
                terminalViewModel.Latitude,
                terminalViewModel.Longitude);
        }
        #endregion

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
