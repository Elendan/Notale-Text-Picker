using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Polygon.ColorPicker.Events;
using Polygon.ColorPicker.Models;
using Polygon.ColorPicker.ViewModels;
using Polygon.Utils.Extensions;

namespace Polygon.ColorPicker.Extensions
{
    public static class PickerViewModelExtensions
    {
        public static void InitializeUi(this PickerViewModel model)
        {
            model.ChangeFamilyLevelColorContent = "Change family level command";
            model.ColorDisplayContent = "Color hex preview";
            model.PickerButtonContent = "Choose color";
            model.ChangeGmTagButtonContent = "Change GM Tag";
            model.ChangeRightClickColorContent = "Change right click text";
            model.ColorBrush = Brushes.Black;
            model.FamilyLevels = new ObservableCollection<FamilyLevelModel>();
            InitializeFamilyLevels(model);
        }

        private static void InitializeFamilyLevels(PickerViewModel model)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key.Contains("FamilyLevel") && !string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
                {
                    model.FamilyLevels.Add(new FamilyLevelModel
                    {
                        Key = key,
                        HexValue = ConfigurationManager.AppSettings[key]
                    });
                }
            }

            model.SelectedFamilyLevel = model.FamilyLevels.FirstOrDefault();
        }
    }
}
