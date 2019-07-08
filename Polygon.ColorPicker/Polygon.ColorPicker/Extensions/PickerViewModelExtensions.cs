using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Polygon.ColorPicker.ViewModels;
using Polygon.Utils.Extensions;

namespace Polygon.ColorPicker.Extensions
{
    public static class PickerViewModelExtensions
    {
        private static readonly string _currentColor = ConfigurationManager.AppSettings["CurrentColor"];

        public static void InitializeKeys(this PickerViewModel model)
        {
            model.ColorDisplayContent = _currentColor;
            model.PickerButtonContent = "Choose color";
            model.ChangeGmTagButtonContent = "Change GM Tag";
            model.ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + _currentColor));
        }
    }
}
