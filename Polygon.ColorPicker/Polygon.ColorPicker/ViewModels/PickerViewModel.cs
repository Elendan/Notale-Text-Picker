using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Polygon.ColorPicker.Events;
using Polygon.ColorPicker.Interfaces;
using Polygon.Models.Enums;
using Polygon.Utils;

namespace Polygon.ColorPicker.ViewModels
{
    public class PickerViewModel : BaseViewModel, IPageViewModel
    {
        private const string EXE_FILTER = "Executable (NostaleClientX.exe)|NostaleClientX.exe";

        public PickerViewModel()
        {
            var data = ConfigurationManager.AppSettings["NostaleClientXPath"];
            if (string.IsNullOrEmpty(data) || !File.Exists(data))
            {
                MessageBox.Show("Please choose your NostaleClientX.exe");
                var dlg = new OpenFileDialog
                {
                    Filter = EXE_FILTER
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SettingsManager.AddOrUpdateAppSettings("NostaleClientXPath", dlg.FileName);
                }
            }
        }

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
