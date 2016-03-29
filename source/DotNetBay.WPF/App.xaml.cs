using System.Windows;
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
