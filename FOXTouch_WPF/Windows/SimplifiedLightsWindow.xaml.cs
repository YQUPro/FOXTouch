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

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedLightsWindow.xaml
    /// </summary>
    public partial class SimplifiedLightsWindow : Window
    {
        public SimplifiedLightsWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += SimplifiedLightsWindow_MouseLeftButtonDown;
        }

        private void SimplifiedLightsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
