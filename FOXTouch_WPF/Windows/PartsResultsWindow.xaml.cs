using FOXTouch_WPF.ViewModels;
using FOXTouch_WPF.InterfacesDeclarations;
using System.Windows;
using System.Windows.Input;
using GenericComponentsMVVM;
using System.Collections.Generic;
using System;
using FOXTouch_WPF.Views;

namespace FOXTouch_WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour PartsResultsWindow.xaml
    /// </summary>
    public partial class PartsResultsWindow : Window,ILateInitializableDataContextWindow,IRelayCommandReceiver
    {
        public PartsResultsWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += PartsResultsWindow_MouseLeftButtonDown;
        }

        void IRelayCommandReceiver.SetRelayCommands(Dictionary<string, RelayCommand> relayCommands)
        {
            if (DataContext is PartsResultsViewModel viewModel)
            {
                viewModel.Commands = relayCommands;
                Console.WriteLine($"Commands assigned to SimplifiedLightsViewModel with HashCode : {viewModel.GetHashCode()}");
            }
        }

        void ILateInitializableDataContextWindow.InitializeDataContext(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            PartsResultsView PartsResultsView = new PartsResultsView(dataContext);
            MainGrid.Children.Add(PartsResultsView);
        }

        private void PartsResultsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
