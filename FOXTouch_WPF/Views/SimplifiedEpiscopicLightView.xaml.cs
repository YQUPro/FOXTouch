﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessengerService;
using static MessengerService.MessengerServiceMessagesDeclarations;

namespace FOXTouch_WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour SimplifiedEpiscopicLightView.xaml
    /// </summary>
    public partial class SimplifiedEpiscopicLightView : UserControl
    {
        public SimplifiedEpiscopicLightView(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;

            Messenger.Instance.Subscribe<WindowClosedMessage>(OnMessage_WindowClosed);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Gérer le changement de valeur du slider
            double value = e.NewValue;
            // Par exemple, mettre à jour une propriété liée ou effectuer une action spécifique
        }

        private void OnMessage_WindowClosed(WindowClosedMessage message)
        {
            //if (message.From == )
        }
    }
}
