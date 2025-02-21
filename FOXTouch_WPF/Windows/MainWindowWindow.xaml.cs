using FOXTouch_WPF.InterfacesDeclarations;
using FOXTouch_WPF.Views;
using System.Windows;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindowWindow : Window, ILateInitializableDataContextWindow
    {
        public MainWindowWindow()
        {
            InitializeComponent();
        }

        public void InitializeDataContext(object dataContext)
        {
            DataContext = dataContext;

            // Assigner le DataContext à SimplifiedLightsView et l'ajouter au Grid
            MainWindowView mainWindowViewView = new MainWindowView(dataContext);
            MainGrid.Children.Add(mainWindowViewView);
        }
    }
}
