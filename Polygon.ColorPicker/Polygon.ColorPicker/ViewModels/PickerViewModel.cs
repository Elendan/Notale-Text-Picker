using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Polygon.ColorPicker.Events;
using Polygon.ColorPicker.Interfaces;
using Polygon.Models.Enums;
using Polygon.ColorPicker.Extensions;
using Polygon.Utils;
using Polygon.Utils.Extensions;

namespace Polygon.ColorPicker.ViewModels
{
    public class PickerViewModel : BaseViewModel, IPageViewModel
    {
        public PickerViewModel()
        {
            if (!IsAdministrator)
            {
                MessageBox.Show($"Please run this program as administrator");
                Application.Exit();
            }

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
                    _nostalePath = dlg.FileName;
                    SettingsManager.AddOrUpdateAppSettings("NostaleClientXPath", dlg.FileName);
                }
            }
            else
            {
                _nostalePath = data;
            }
        }

        private readonly string _nostalePath = string.Empty;

        public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        private const string EXE_FILTER = "Executable (NostaleClientX.exe)|NostaleClientX.exe";

        private string _changeGmTagButtonContent;

        public string ChangeGmTagButtonContent
        {
            get => _changeGmTagButtonContent;
            set
            {
                _changeGmTagButtonContent = value;
                OnPropertyChanged(nameof(ChangeGmTagButtonContent));
            }
        }

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
                        ColorBrush = new SolidColorBrush(dialog.CreateColorFromDialog());
                    }

                    ColorDisplayContent = dialog.CreateColorFromDialog().ToNostaleFormat();
                }));
            }
        }

        private ICommand _changeGmTagCommand;

        public ICommand ChangeGmTagCommand
        {
            get
            {
                return _changeGmTagCommand ?? (_changeGmTagCommand = new RelayCommand(x =>
                {
                    var backupName = "LauncherBackup";
                    var directory = _nostalePath.FindDirectory();

                    if (!Directory.Exists(directory + backupName))
                    {
                        Directory.CreateDirectory(directory + backupName);
                    }

                    var currentDirectoryTime = directory + backupName + $"\\{DateTime.Now:yyyyMdd_HHmmss}";

                    Directory.CreateDirectory(currentDirectoryTime);

                    File.Copy(_nostalePath, currentDirectoryTime + $"\\{_nostalePath.Split('\\').Last()}", true);

                    MessageBox.Show(directory + DateTime.Now.ToString("yyyyMdd_HH:mm:ss"));
                }));
            }
        }
    }
}
