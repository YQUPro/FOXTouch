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
    /// Logique d'interaction pour SimplifiedDiascopicLightWindow.xaml
    /// </summary>
    public partial class SimplifiedDiascopicLightWindow : Window, IRelayCommandReceiver, ILateInitializableDataContextWindow
    {
        public SimplifiedDiascopicLightWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += SimplifiedDiascopicLightWindow_MouseLeftButtonDown;
        }

        void IRelayCommandReceiver.SetRelayCommands(Dictionary<string, RelayCommand> relayCommands)
        {
            if (DataContext is SimplifiedDiascopicLightViewModel viewModel)
            {
                viewModel.Commands = relayCommands;
            }
        }

        void ILateInitializableDataContextWindow.InitializeDataContext(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            SimplifiedDiascopicLightView simplifiedDiascopicLightView = new SimplifiedDiascopicLightView(dataContext);
            MainGrid.Children.Add(simplifiedDiascopicLightView);
        }

        private void SimplifiedDiascopicLightWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
