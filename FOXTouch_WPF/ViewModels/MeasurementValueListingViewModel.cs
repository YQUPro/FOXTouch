using FOXTouchEnumerations.FeaturesType;
using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;
using static FOXTouchEnumerations.Tolerancing.TolerancingModeEnumerations;

namespace FOXTouch_WPF.ViewModels
{
    public class MeasurementValueListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<MeasurementValueListingItemViewModel> _measurementValueListingItemViewModels;
        public IEnumerable<MeasurementValueListingItemViewModel> MeasurementValueListingItemViewModels => _measurementValueListingItemViewModels;
        public ICommand AddMeasurementValueCommand { get; }
        public ICommand RemoveMeasurementValueCommand { get; }

        public int NumberOfMeasurements
        {
            get
            {
                int result = 0;
                foreach (MeasurementValueListingItemViewModel measurement in _measurementValueListingItemViewModels)
                {
                    result = Math.Max(result, measurement.Values.Count);
                }
                return result;
            }
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(NumberOfMeasurements));
        }

        public MeasurementValueListingViewModel()
        {
            _measurementValueListingItemViewModels = new ObservableCollection<MeasurementValueListingItemViewModel>();

            _measurementValueListingItemViewModels.Add(
                new MeasurementValueListingItemViewModel(
                    "Value_01",
                    new List<string> {
                        (3.14f).ToString("F3"),
                        (6.28f).ToString("F3"),
                        (9.42f).ToString("F3"),
                        (12.56f).ToString("F3"),
                        (12.70f).ToString("F3"),
                        (9.42f).ToString("F3"),
                        (3.09999f).ToString("F3"),
                    },
                    "mm",
                    3.1f,
                    ETolerancingMode.DoubleTolerancing,
                    3f,
                    -0.15f,
                    FeatureTypeEnumerations.EResultFeatureType.Type01)
                );
            _measurementValueListingItemViewModels.Add(
                new MeasurementValueListingItemViewModel(
                    "Value_02",
                    new List<string> {
                        (2.14f).ToString("F3"),
                        (4.28f).ToString("F3"),
                        (6.42f).ToString("F3"),
                        (8.56f).ToString("F3"),
                        (8.70f).ToString("F3"),
                        (8.70f).ToString("F3"),
                        (10.75411239f).ToString("F3")
                    },
                    "mm",
                    4.3f,
                    ETolerancingMode.NoTolerancing,
                    0f,
                    0f,
                    FeatureTypeEnumerations.EResultFeatureType.Type02)
                );
            _measurementValueListingItemViewModels.Add(
                new MeasurementValueListingItemViewModel(
                    "Value_03",
                    new List<string> {
                        (1.14f).ToString("F3"),
                        (2.28f).ToString("F3"),
                        (42.42f).ToString("F3"),
                        (3.42f).ToString("F3"),
                        (42.56f).ToString("F3"),
                        (42.56f).ToString("F3"),
                        ""
                    },
                    "mm",
                    3.4f,
                    ETolerancingMode.SimpleUpperTolerancing,
                    2.1f,
                    0f,
                    FeatureTypeEnumerations.EResultFeatureType.Type01)
                );
            _measurementValueListingItemViewModels.Add(
                new MeasurementValueListingItemViewModel(
                    "Value_05",
                    new List<string> {
                        (1.14f).ToString("F3"),
                        (2.28f).ToString("F3"),
                        (42.42f).ToString("F3"),
                        (3.42f).ToString("F3"),
                        (42.56f).ToString("F3"),
                        (3.42f).ToString("F3"),
                        (25.4451f).ToString("F3")
                    },
                    "mm",
                    1f,
                    ETolerancingMode.SimpleUpperTolerancing,
                    6f,
                    0f,
                    FeatureTypeEnumerations.EResultFeatureType.Type03)
                );

            // Ajout des commandes
            AddMeasurementValueCommand = new RelayCommandWithResult<string,EFunctionErrorCode>(AddMeasurementValues);
            RemoveMeasurementValueCommand = new RelayCommandWithResult<MeasurementValueListingItemViewModel,EFunctionErrorCode >(RemoveMeasurementValue);
        }


        public static Dictionary<string, string> ParseStringToDictionary(string input)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            // Séparer les paires clé-valeur
            string[] pairs = input.Split(Properties.Settings.Default.MeasurementValuesSeparator[0]);

            foreach (string pair in pairs)
            {
                // Séparer la clé et la valeur
                string[] keyValue = pair.Split(Properties.Settings.Default.NameValueSeparator[0]);
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    string value = keyValue[1];
                    result[key] = value;
                }
            }

            return result;
        }

        private EFunctionErrorCode AddMeasurementValues(string concatenatedValues)
        {
            if (string.IsNullOrEmpty(concatenatedValues))
                return EFunctionErrorCode.Failed;

            Dictionary<string, string> parsedValues = ParseStringToDictionary(concatenatedValues);

            // Créer un dictionnaire pour un accès rapide par nom
            var itemViewModelDictionary = _measurementValueListingItemViewModels
                .ToDictionary(item => item.Name);

            foreach (var item in parsedValues)
            {
                if (itemViewModelDictionary.TryGetValue(item.Key, out var itemViewModel))
                {
                    itemViewModel.AddMeasurementValue(item.Value);
                }
            }
            UpdateProperties();
            return EFunctionErrorCode.Success;
        }

        private EFunctionErrorCode RemoveMeasurementValue(MeasurementValueListingItemViewModel item)
        {
            if (_measurementValueListingItemViewModels.Contains(item))
            {
                _measurementValueListingItemViewModels.Remove(item);
                UpdateProperties();
                return EFunctionErrorCode.Success;
            }
            return EFunctionErrorCode.Failed;
        }
    }

    
}
