using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOXTouch_WPF.ViewModels
{
    public class SimplifiedEpiscopicLightViewModel : ViewModelBase
    {
        public Dictionary<string, RelayCommand> Commands { get; set; }

        public void ExecuteEpiscopicLightCommand()
        {
            // Action pour la commande des lumières épiscopiques
            Console.WriteLine("Executing Episcopic Light Command");
        }
    }
}
