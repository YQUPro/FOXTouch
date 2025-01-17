using System.Windows;
using System.Windows.Input;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour PartsResultsWindow.xaml
    /// </summary>
    public partial class PartsResultsWindow : Window
    {
        public PartsResultsWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += PartsResultsWindow_MouseLeftButtonDown;
        }

        private void PartsResultsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
