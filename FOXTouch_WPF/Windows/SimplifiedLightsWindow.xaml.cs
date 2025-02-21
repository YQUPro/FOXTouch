using FOXTouch_WPF.InterfacesDeclarations;
using FOXTouch_WPF.ViewModels;
using FOXTouch_WPF.Views;
using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FOXTouch_WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedLightsWindow.xaml
    /// </summary>
    public partial class SimplifiedLightsWindow : Window, IRelayCommandReceiver, ILateInitializableDataContextWindow
    {
        public SimplifiedLightsWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += SimplifiedLightsWindow_MouseLeftButtonDown;
        }

        void IRelayCommandReceiver.SetRelayCommands(Dictionary<string, RelayCommand> relayCommands)
        {
            if (DataContext is SimplifiedLightsViewModel viewModel)
            {
                viewModel.Commands = relayCommands;
            }
        }

        void ILateInitializableDataContextWindow.InitializeDataContext(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            SimplifiedLightsView simplifiedLightsView = new SimplifiedLightsView(dataContext);
            MainGrid.Children.Add(simplifiedLightsView);
        }

        private void SimplifiedLightsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
