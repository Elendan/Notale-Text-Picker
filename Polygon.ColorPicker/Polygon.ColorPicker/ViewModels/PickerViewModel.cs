using System;
using System.Collections.ObjectModel;
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
        #region Constructor

        public PickerViewModel()
        {
            if (!IsAdministrator)
            {
                MessageBox.Show($"Please run this program as administrator");
                Application.Exit();
            }

            this.InitializeUi();
            var data = ConfigurationManager.AppSettings["LauncherPath"];
            if (string.IsNullOrEmpty(data) || !File.Exists(data))
            {
                MessageBox.Show("Please choose your Launcher");
                var dlg = new OpenFileDialog
                {
                    Filter = EXE_FILTER
                };
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _nostalePath = dlg.FileName;
                SettingsManager.AddOrUpdateAppSettings("LauncherPath", dlg.FileName);
            }
            else
            {
                _nostalePath = data;
            }
        }

        #endregion

        #region Members

        private readonly string _oldRightClickColor = ConfigurationManager.AppSettings["CurrentRightClickColor"];

        private readonly string _oldGmColor = ConfigurationManager.AppSettings["CurrentGmColor"];

        private readonly string _nostalePath = string.Empty;

        private static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        private const string EXE_FILTER = "Executable (*.exe)|*.exe";

        #endregion

        #region UpdateableProperties

        private string _changeRightClickColorContent;

        public string ChangeRightClickColorContent
        {
            get => _changeRightClickColorContent;
            set
            {
                _changeRightClickColorContent = value;
                OnPropertyChanged(nameof(ChangeRightClickColorContent));
            }
        }

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

        #endregion

        #region ICommands

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
                    var previous = "00FFFFFF";
                    UpdateInformationFromPattern("CurrentGmColor", $"{previous}{_oldGmColor}", previous.Length);
                }));
            }
        }

        private ICommand _changeRightClickColorCommand;

        public ICommand ChangeRightClickColorCommand
        {
            get
            {
                return _changeRightClickColorCommand ?? (_changeRightClickColorCommand = new RelayCommand(x =>
                {
                    var previous = "C7466F";
                    UpdateInformationFromPattern("CurrentRightClickColor", $"{previous}{_oldRightClickColor}", previous.Length);
                }));
            }
        }

        private ICommand _resetOptionsCommand;

        public ICommand ResetOptionsCommand
        {
            get
            {
                return _resetOptionsCommand ?? (_resetOptionsCommand = new RelayCommand(x =>
                {
                    SettingsManager.AddOrUpdateAppSettings("LauncherPath", string.Empty);
                    SettingsManager.AddOrUpdateAppSettings("CurrentGmColor", "FF6CBFFF");
                    SettingsManager.AddOrUpdateAppSettings("CurrentRightClickColor", "78C4F5FF");
                    MessageBox.Show("Settings have been reset. This program will close");
                    System.Windows.Application.Current.Shutdown();
                }));
            }
        }

        #endregion

        #region Methods

        private void UpdateInformationFromPattern(string appSettingKey, string pattern, int substringIndex)
        {
            const string backupName = "LauncherBackup";
            var directory = _nostalePath.FindDirectory();

            if (!Directory.Exists(directory + backupName))
            {
                Directory.CreateDirectory(directory + backupName);
            }

            var currentDirectoryTime = directory + backupName + $"\\{DateTime.Now:yyyyMdd_HHmmss}";

            Directory.CreateDirectory(currentDirectoryTime);

            File.Copy(_nostalePath, currentDirectoryTime + $"\\{_nostalePath.Split('\\').Last()}", true);

            var hexFinder = new HexFinder(_nostalePath, ColorDisplayContent);

            if (!hexFinder.ReplaceColorPattern(pattern, substringIndex))
            {
                MessageBox.Show("An error occurred !\nplease reset settings and/or restore a backup of your launcher !");
                return;
            }

            SettingsManager.AddOrUpdateAppSettings(appSettingKey, ColorDisplayContent);
            MessageBox.Show("Backup created, value changed successfully !");
        }

        #endregion
    }
}
