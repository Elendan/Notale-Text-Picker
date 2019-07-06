using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Polygon.ColorPicker.ViewModels;

namespace Polygon.ColorPicker.Extensions
{
    public static class PickerViewModelExtensions
    {
        public static void InitializeKeys(this PickerViewModel model)
        {
            model.ColorDisplayContent = "Color Test";
            model.PickerButtonContent = "Choose color";
            model.ColorBrush = Brushes.Purple;
        }
    }
}
