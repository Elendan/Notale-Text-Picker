using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Polygon.ColorPicker.Events;
using Polygon.ColorPicker.Interfaces;
using Polygon.Models.Enums;
using Polygon.ColorPicker.Extensions;
using Polygon.Utils;

namespace Polygon.ColorPicker.ViewModels
{
    public class PickerViewModel : BaseViewModel, IPageViewModel
    {
        public PickerViewModel()
        {
            this.InitializeKeys();
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

        private const string EXE_FILTER = "Executable (NostaleClientX.exe)|NostaleClientX.exe";

        private string _pickerButtonContent;

        public string PickerButtonContent
        {
            get => _pickerButtonContent;
            set
            {
                _pickerButtonContent = value;
                OnPropertyChanged(nameof(PickerButtonContent));
            }
        }

        private string _colorDisplayContent;

        public string ColorDisplayContent
        {
            get => _colorDisplayContent;
            set
            {
                _colorDisplayContent = value;
                OnPropertyChanged(nameof(ColorDisplayContent));
            }
        }

        private Brush _colorBrush;

        public Brush ColorBrush
        {
            get => _colorBrush;
            set
            {
                _colorBrush = value;
                OnPropertyChanged(nameof(ColorBrush));
            }
        }

        private ICommand _chooseColorCommand;

        public ICommand ChooseColorCommand
        {
            get
            {
                return _chooseColorCommand ?? (_chooseColorCommand = new RelayCommand(x =>
                {
                    var dialog = new ColorDialog();

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ColorBrush = new SolidColorBrush(new Color
                        {
                            A = dialog.Color.A,
                            B = dialog.Color.B,
                            G = dialog.Color.G,
                            R = dialog.Color.R
                        });
                    }
                }));
            }
        }
    }
}
