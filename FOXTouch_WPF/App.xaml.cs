﻿using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ajout des gestionnaires d'événements pour les erreurs de liaison et les exceptions non gérées
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;

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

        private void FrameworkElement_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is INotifyPropertyChanged newContext)
            {
                newContext.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == "Commands")
                    {
                        // Logique pour vérifier les commandes
                    }
                };
            }
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Erreur non gérée", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
