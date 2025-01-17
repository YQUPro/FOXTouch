using GenericComponentsMVVM;

namespace FOXTouch_WPF.ViewModels
{
    public class PartsResultsViewModel : ViewModelBase
    {
        public MeasurementValueListingViewModel MeasurementValueListingViewModel { get; }

        public PartsResultsViewModel()
        {
            MeasurementValueListingViewModel = new MeasurementValueListingViewModel();
        }
    }
}
