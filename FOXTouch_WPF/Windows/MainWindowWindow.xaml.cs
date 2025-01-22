using FOXTouch_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FOXTouch_WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindowWindow : Window
    {
        public MainWindowWindow()
        {
            this.DataContextChanged += FrameworkElement_DataContextChanged;
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
        }

        private void FrameworkElement_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is INotifyPropertyChanged newContext)
            {
                newContext.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == "Commands")
                    {
                        // Logique pour vérifier les commandes
                    }
                };
            }
        }
    }
}
