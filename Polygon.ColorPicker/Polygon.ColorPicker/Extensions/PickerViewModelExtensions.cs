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
        public static void InitializeKeys(this PickerViewModel model)
        {
            model.ColorDisplayContent = "Color hex preview";
            model.PickerButtonContent = "Choose color";
            model.ChangeGmTagButtonContent = "Change GM Tag";
            model.ChangePrincipalRightClickTextContent = "Change right click text";
            model.ColorBrush = Brushes.Black;
        }
    }
}
