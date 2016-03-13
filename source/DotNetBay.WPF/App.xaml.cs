using DotNetBay.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.FileStorage;
using DotNetBay.Model;
using DotNetBay.WPF.View;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            // Demo Einträge erstellen lassen
            UIHelper.CreateDemoData();
            // MainView laden und anzeigen
            MainWindow view = new MainWindow { DataContext = new MainWindowViewModel() };
            view.Show();
        }
    }
}
