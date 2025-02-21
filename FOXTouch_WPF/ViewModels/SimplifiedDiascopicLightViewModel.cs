using GenericComponentsMVVM;
using MessengerService;
using static MessengerService.MessengerServiceMessagesDeclarations;

namespace FOXTouch_WPF.ViewModels
{
    public class SimplifiedDiascopicLightViewModel : ViewModelBase
    {
        private double _sliderValue;
        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                    Messenger.Instance.Send(new SliderValueChangedMessage(this,_sliderValue));
                }
            }
        }

        public SimplifiedDiascopicLightViewModel()
        {
        }
    }
}
