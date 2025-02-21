using System.Windows.Controls;

namespace FOXTouch_WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        public MainWindowView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
