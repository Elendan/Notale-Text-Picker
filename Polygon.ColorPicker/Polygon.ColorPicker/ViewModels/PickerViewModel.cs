using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Polygon.ColorPicker.Events;
using Polygon.ColorPicker.Interfaces;
using Polygon.Models.Enums;

namespace Polygon.ColorPicker.ViewModels
{
    public class PickerViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _testPattern;

        public ICommand TestPattern
        {
            get
            {
                return _testPattern ?? (_testPattern = new RelayCommand(x =>
                {
                    Mediator.Notify(ScreenEventType.GoToMainView, null);
                }));
            }
        }
    }
}
