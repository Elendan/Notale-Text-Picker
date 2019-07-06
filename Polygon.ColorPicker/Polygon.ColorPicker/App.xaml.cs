using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Polygon.ColorPicker.ViewModels;

namespace Polygon.ColorPicker
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// OnStartup Event override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var app = new MainWindow();
            var context = new MainWindowViewModel();
            app.DataContext = context;
            app.Show();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            OnStartup(e);
        }
    }
}
