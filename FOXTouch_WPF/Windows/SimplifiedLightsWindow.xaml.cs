using FOXTouch_WPF.InterfacesDeclarations;
using FOXTouch_WPF.ViewModels;
using FOXTouch_WPF.Views;
using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedLightsWindow.xaml
    /// </summary>
    public partial class SimplifiedLightsWindow : Window, IRelayCommandReceiver
    {
        public SimplifiedLightsWindow()
        {
            InitializeComponent();

            this.DataContextChanged += FrameworkElement_DataContextChanged;

            this.MouseLeftButtonDown += SimplifiedLightsWindow_MouseLeftButtonDown;
        }

        void IRelayCommandReceiver.SetRelayCommands(Dictionary<string, RelayCommand> relayCommands)
        {
            if (DataContext is SimplifiedLightsViewModel viewModel)
            {
                viewModel.Commands = relayCommands;
                Console.WriteLine($"Commands assigned to SimplifiedLightsViewModel with HashCode : {viewModel.GetHashCode()}");
            }
        }

        public void Initialize(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            SimplifiedLightsView simplifiedLightsView = new SimplifiedLightsView(dataContext);
            MainGrid.Children.Add(simplifiedLightsView);
        }

        private void FrameworkElement_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is INotifyPropertyChanged newContext)
            {
                newContext.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == "Commands")
                    {
                        Console.WriteLine("args == Commands");
                        // Logique pour vérifier les commandes
                    }
                };
            }
        }

        private void SimplifiedLightsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
