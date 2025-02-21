using FOXTouch_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FOXTouch_WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedLightsView.xaml
    /// </summary>
    public partial class SimplifiedLightsView : UserControl
    {
        public SimplifiedLightsView(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }

        private void SimplifiedLightsRadioButton_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && DataContext is SimplifiedLightsViewModel viewModel)
            {
                var position = radioButton.PointToScreen(new Point(0, 0));
                switch (radioButton.Name)
                {
                    case nameof(SimplifiedEpiscopicLightRadioButton):
                        viewModel.Commands["ToggleSimplifiedEpiscopicLightViewCommand"].Execute(null);
                        //MessengerService.Messenger.Instance.Send < new MessengerService.MessengerServiceMessagesDeclarations.WindowClosedMessage()>;
                        //(Application.Current.MainWindow.DataContext as MainWindowViewModel)?.ToggleSimplifiedEpiscopicLightView(position);
                        break;
                    case nameof(SimplifiedAuxiliaryLightRadioButton):
                        viewModel.Commands["ToggleSimplifiedAuxiliaryLightViewCommand"].Execute(null);
                        break;
                    case nameof(SimplifiedDiascopicLightRadioButton):
                        viewModel.Commands["ToggleSimplifiedDiascopicLightViewCommand"].Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
