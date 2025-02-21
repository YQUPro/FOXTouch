using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FOXTouch_WPF.InterfacesDeclarations;
using FOXTouch_WPF.ViewModels;
using FOXTouch_WPF.Views;
using GenericComponentsMVVM;

namespace FOXTouch_WPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedEpiscopicLightWindow.xaml
    /// </summary>
    public partial class SimplifiedAuxiliaryLightWindow : Window, ILateInitializableDataContextWindow, IRelayCommandReceiver
    {
        public SimplifiedAuxiliaryLightWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += SimplifiedAuxiliaryLightWindow_MouseLeftButtonDown;
        }

        void IRelayCommandReceiver.SetRelayCommands(Dictionary<string, RelayCommand> relayCommands)
        {
            if (DataContext is SimplifiedEpiscopicLightViewModel viewModel)
            {
                viewModel.Commands = relayCommands;
            }
        }

        void ILateInitializableDataContextWindow.InitializeDataContext(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            SimplifiedAuxiliaryLightView simplifiedAuxiliaryLightView = new SimplifiedAuxiliaryLightView(dataContext);
            MainGrid.Children.Add(simplifiedAuxiliaryLightView);
        }

        private void SimplifiedAuxiliaryLightWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
