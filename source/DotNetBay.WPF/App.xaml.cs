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

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {

        public App() {
            // Demo Einträge erstellen lassen
            UIHelper.CreateDemoData();
        }
        
    }
}
