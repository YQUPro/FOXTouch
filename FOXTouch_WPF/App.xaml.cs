using FOXTouch_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialisation du splash screen
            SplashScreenWindow splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            // Simule un temps de travail
            Thread.Sleep(3000);

            // Initialisation de la fenêtre principale
            MainWindowWindow mainWindow = new MainWindowWindow();
            mainWindow.Show();

            base.OnStartup(e);

            // Ferme le splash screen
            splashScreen.Close();
        }
    }
}
