using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Polygon.ColorPicker.Interfaces;
using Polygon.Models.Enums;

namespace Polygon.ColorPicker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            /*
             * Constructor adds the new ViewModels
             */
            PageViewModels.Add(new PickerViewModel());

            /*
             * Constructor sets the current view model
             */
            CurrentPageViewModel = PageViewModels[0];

            /*
             * Constructor subscribes to mediator events
             */
            Mediator.Subscribe(ScreenEventType.GoToMainView, OnGoToMainView);
        }

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        /// <summary>
        /// List of view models
        /// </summary>
        public List<IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>());

        /// <summary>
        /// Current view model
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        /// <summary>
        ///     Switches between ViewModels
        /// </summary>
        /// <param name="viewModel"></param>
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        private static void OnGoToMainView(object obj)
        {
            MessageBox.Show("MVVM PATTERN WORKS");
        }
    }
}
