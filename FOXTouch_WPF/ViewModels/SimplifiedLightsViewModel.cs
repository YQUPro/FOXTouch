using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GenericComponentsMVVM;

namespace FOXTouch_WPF.ViewModels
{
    public class SimplifiedLightsViewModel : ViewModelBase
    {
        public ICommand ToggleSimplifiedEpiscopicLightViewCommand { get; }

        public SimplifiedLightsViewModel(ICommand toggleSimplifiedEpiscopicLightViewCommand)
        {
            ToggleSimplifiedEpiscopicLightViewCommand = toggleSimplifiedEpiscopicLightViewCommand;
        }

    }
}
