using FOXTouch_WPF.ViewModels;
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
    }
}
