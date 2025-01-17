using FOXTouchLightingBoards;
using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FOXTouch_WPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Dictionary<string, Window> openWindows;
        private readonly PartsResultsViewModel partsResultsViewModel;
        private readonly SimplifiedLightsViewModel simplifiedLightsViewModel;

        private readonly FoxLightingBoardControllerViewModel foxLightingBoardControllerViewModel;

        #region Définition des commutateurs permettant l'affichage des boutons du bandeau de gauche
        private bool _isCameraContextButtonVisible = true;
        public bool IsCameraContextButtonVisible
        {
            get { return _isCameraContextButtonVisible; }
            set
            {
                _isCameraContextButtonVisible = value;
                OnPropertyChanged(nameof(IsCameraContextButtonVisible));
            }
        }

        private bool _isReportsButtonVisible = true;
        public bool IsReportsButtonVisible
        {
            get { return _isReportsButtonVisible; }
            set
            {
                _isReportsButtonVisible = value;
                OnPropertyChanged(nameof(IsReportsButtonVisible));
            }
        }

        private bool _isBatchMeasurementsButtonVisible = true;
        public bool IsBatchMeasurementsButtonVisible
        {
            get { return _isBatchMeasurementsButtonVisible; }
            set
            {
                _isBatchMeasurementsButtonVisible = value;
                OnPropertyChanged(nameof(IsBatchMeasurementsButtonVisible));
            }
        }

        private bool _isStatisticsButtonVisible = true;
        public bool IsStatisticsButtonVisible
        {
            get { return _isStatisticsButtonVisible; }
            set
            {
                _isStatisticsButtonVisible = value;
                OnPropertyChanged(nameof(IsStatisticsButtonVisible));
            }
        }

        private bool _isCADManagementButtonVisible = true;
        public bool IsCADManagementButtonVisible
        {
            get { return _isCADManagementButtonVisible; }
            set
            {
                _isCADManagementButtonVisible = value;
                OnPropertyChanged(nameof(IsCADManagementButtonVisible));
            }
        }

        private bool _isProgramContentButtonVisible = true;
        public bool IsProgramContentButtonVisible
        {
            get { return _isProgramContentButtonVisible; }
            set
            {
                _isProgramContentButtonVisible = value;
                OnPropertyChanged(nameof(IsProgramContentButtonVisible));
            }
        }

        private bool _isFeaturesResultsButtonVisible = true;
        public bool IsFeaturesResultsButtonVisible
        {
            get { return _isFeaturesResultsButtonVisible; }
            set
            {
                _isFeaturesResultsButtonVisible = value;
                OnPropertyChanged(nameof(IsFeaturesResultsButtonVisible));
            }
        }

        private bool _isSimplifiedLightsButtonVisible = true;
        public bool IsSimplifiedLightsButtonVisible
        {
            get { return _isSimplifiedLightsButtonVisible; }
            set
            {
                _isSimplifiedLightsButtonVisible = value;
                OnPropertyChanged(nameof(IsSimplifiedLightsButtonVisible));
            }
        }

        private bool _isFunctionsButtonVisible = true;
        public bool IsFunctionsButtonVisible
        {
            get { return _isFunctionsButtonVisible; }
            set
            {
                _isFunctionsButtonVisible = value;
                OnPropertyChanged(nameof(IsFunctionsButtonVisible));
            }
        }
        #endregion

        #region Définition des commutateurs permettant l'affichage des boutons du bandeau de droite
        private bool _isCameraLiveButtonVisible = true;
        public bool IsCameraLiveButtonVisible
        {
            get { return _isCameraLiveButtonVisible; }
            set
            {
                _isCameraLiveButtonVisible = value;
                OnPropertyChanged(nameof(IsCameraLiveButtonVisible));
            }
        }

        private bool _isOpenPictureButtonVisible = true;
        public bool IsOpenPictureButtonVisible
        {
            get { return _isOpenPictureButtonVisible; }
            set
            {
                _isOpenPictureButtonVisible = value;
                OnPropertyChanged(nameof(IsOpenPictureButtonVisible));
            }
        }

        private bool _isSaveCameraPictureButtonVisible = true;
        public bool IsSaveCameraPictureButtonVisible
        {
            get { return _isSaveCameraPictureButtonVisible; }
            set
            {
                _isSaveCameraPictureButtonVisible = value;
                OnPropertyChanged(nameof(IsSaveCameraPictureButtonVisible));
            }
        }

        private bool _isSaveApplicationScreenshotButtonVisible = true;
        public bool IsSaveApplicationScreenshotButtonVisible
        {
            get { return _isSaveApplicationScreenshotButtonVisible; }
            set
            {
                _isSaveApplicationScreenshotButtonVisible = value;
                OnPropertyChanged(nameof(IsSaveApplicationScreenshotButtonVisible));
            }
        }

        private bool _isCameraAutofocus_FunctionButtonVisible = true;
        public bool IsCameraAutofocus_FunctionButtonVisible
        {
            get { return _isCameraAutofocus_FunctionButtonVisible; }
            set
            {
                _isCameraAutofocus_FunctionButtonVisible = value;
                OnPropertyChanged(nameof(IsCameraAutofocus_FunctionButtonVisible));
            }
        }

        private bool _isConfocalAutofocus_FunctionButtonVisible = true;
        public bool IsConfocalAutofocus_FunctionButtonVisible
        {
            get { return _isConfocalAutofocus_FunctionButtonVisible; }
            set
            {
                _isConfocalAutofocus_FunctionButtonVisible = value;
                OnPropertyChanged(nameof(IsConfocalAutofocus_FunctionButtonVisible));
            }
        }
        #endregion

        #region Définition des ICommand disponibles
        public ICommand TogglePartsResultsViewCommand { get; }
        public ICommand ToggleSimplifiedLightsViewCommand { get; }
        public ICommand ToggleSimplifiedEpiscopicLightViewCommand { get; }
        public ICommand ToggleSimplifiedDiascopicLightViewCommand { get; }
        public ICommand ToggleSimplifiedAuxialiaryLightViewCommand { get; }
        public ICommand ExitApplicationCommand { get; }
        public ICommand MinimizeApplicationCommand { get; }

        public ICommand AddMeasurementCommand_DEV { get; }
        #endregion

        public MainWindowViewModel()
        {
            openWindows = new Dictionary<string, Window>(); // Initialisation du dictionnaire des fenêtres

            #region  Associations de la définition des ICommand et du code source associée
            TogglePartsResultsViewCommand = new RelayCommand(TogglePartsResultsView);
            ToggleSimplifiedLightsViewCommand = new RelayCommand(ToggleSimplifiedLightsView);
            ToggleSimplifiedEpiscopicLightViewCommand = new RelayCommand(ToggleSimplifiedEpiscopicLightView);

            MinimizeApplicationCommand = new RelayCommand(MinimizeApplication);
            ExitApplicationCommand = new RelayCommand(ExitApplication);

            AddMeasurementCommand_DEV = new RelayCommand(AddMeasurement_DEV);
            #endregion

            partsResultsViewModel = new PartsResultsViewModel();
            simplifiedLightsViewModel = new SimplifiedLightsViewModel(this.ToggleSimplifiedEpiscopicLightViewCommand);

            foxLightingBoardControllerViewModel = new FoxLightingBoardControllerViewModel();
            foxLightingBoardControllerViewModel.StartLightingBoardCommand.Execute(null);

            Random r = new Random();
            byte[] rdm = new byte[8];
            r.NextBytes(rdm);
            var parameters = Tuple.Create(
                    Tuple.Create(
                        true, rdm[0],
                        true, rdm[1],
                        true, rdm[2]
                        )
                    , Tuple.Create(
                        true, rdm[3],
                        true, rdm[4],
                        true, rdm[5]
                        )
                    , Tuple.Create(
                        true, rdm[6],
                        true, rdm[7]
                        )
                );
            foxLightingBoardControllerViewModel.UpdateLightsValuesCommand.Execute(parameters);
        }

        private void ToggleSimplifiedLightsView()
        {
            ToggleWindow<SimplifiedLightsWindow>("SimplifiedLightsWindow", null);
        }

        private void ToggleSimplifiedEpiscopicLightView()
        {
            ToggleWindow<SimplifiedEpiscopicLightWindow>("SimplifiedEpiscopicLightWindow", null);
        }

        private void TogglePartsResultsView()
        {
            ToggleWindow<PartsResultsWindow>("PartsResultsWindow", partsResultsViewModel);
        }

        private void MinimizeApplication()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void AddMeasurement_DEV()
        {
            partsResultsViewModel.MeasurementValueListingViewModel.AddMeasurementValueCommand.Execute(GetConcatenatedValuesInput_DEV());
        }

        private void ToggleWindow<T>(string windowKey, object dataContext = null) where T : Window, new()
        {
            if (openWindows.ContainsKey(windowKey))
            {
                var window = openWindows[windowKey];
                if (window.IsVisible)
                {
                    window.Hide();
                }
                else
                {
                    window.DataContext = dataContext;
                    window.Show();
                    window.Activate();
                }
            }
            else
            {
                T window = new T
                {
                    Owner = Application.Current.MainWindow // Définir la fenêtre principale comme propriétaire
                };
                window.Closed += (s, args) => openWindows.Remove(windowKey);
                openWindows[windowKey] = window;
                window.DataContext = dataContext;
                window.Show();
            }
        }

        private string GetConcatenatedValuesInput_DEV()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            double minValue = 1.0;
            double maxValue = 10.0;
            string storageFormat = ("F" + Properties.Settings.Default.SignificantDecimalPlace_Display);

            stringBuilder.Append("Value_01=" + (random.NextDouble() * (maxValue - minValue) + minValue).ToString(storageFormat) + ';');
            stringBuilder.Append("Value_02=" + (random.NextDouble() * (maxValue - minValue) + minValue).ToString(storageFormat) + ';');
            stringBuilder.Append("Value_03=" + (random.NextDouble() * (maxValue - minValue) + minValue).ToString(storageFormat) + ';');
            stringBuilder.Append("Value_04=" + (random.NextDouble() * (maxValue - minValue) + minValue).ToString(storageFormat) + ';');
            stringBuilder.Append("Value_05=" + (random.NextDouble() * (maxValue - minValue) + minValue).ToString(storageFormat) + ';');

            return stringBuilder.ToString();
        }
    }
}
