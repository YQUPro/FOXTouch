﻿using FOXTouch_WPF.InterfacesDeclarations;
using FOXTouch_WPF.Windows;
using FOXTouchLightingBoards;
using GenericComponentsMVVM;
using MessengerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static MessengerService.MessengerServiceMessagesDeclarations;

namespace FOXTouch_WPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private const string SimplifiedLightsWindowKey = "SimplifiedLightsWindowKey";
        private const string SimplifiedEpiscopicLightWindowKey = "SimplifiedEpiscopicLightWindowKey";
        private const string SimplifiedDiascopicLightWindowKey = "SimplifiedDiascopicLightWindowKey";
        private const string SimplifiedAuxiliaryLightWindowKey = "SimplifiedAuxiliaryLightWindowKey";
        private const string PartsResultsWindowKey = "PartsResultsWindowKey";


        private Dictionary<string, Window> openWindows;
        private readonly Dictionary<string, bool> windowVisibility = new Dictionary<string, bool>
        {
            { SimplifiedLightsWindowKey, false },
            { PartsResultsWindowKey, false },
            { SimplifiedEpiscopicLightWindowKey, false },
            { SimplifiedDiascopicLightWindowKey, false },
            { SimplifiedAuxiliaryLightWindowKey, false }
        };

        // Initialisation des ViewModels
        private readonly FoxLightingBoardControllerViewModel foxLightingBoardControllerViewModel;
        private readonly SimplifiedLightsViewModel simplifiedLightsViewModel;
        private readonly SimplifiedEpiscopicLightViewModel simplifiedEpiscopicLightViewModel;
        private readonly SimplifiedDiascopicLightViewModel simplifiedDiascopicLightViewModel;
        private readonly SimplifiedAuxiliaryLightViewModel simplifiedAuxiliaryLightViewModel;
        private readonly PartsResultsViewModel partsResultsViewModel;

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

        public ICommand AddMeasurementCommand_DEV { get; }
        #endregion

        #region Booléens de gestion de la visibilité des fenêtres
        public bool IsSimplifiedLightsWindowVisible
        {
            get => windowVisibility[SimplifiedLightsWindowKey];
            set
            {
                if (windowVisibility[SimplifiedLightsWindowKey] != value)
                {
                    windowVisibility[SimplifiedLightsWindowKey] = value;
                    OnPropertyChanged(nameof(IsSimplifiedLightsWindowVisible));
                    if (value)
                        ShowWindow<SimplifiedLightsWindow>(SimplifiedLightsWindowKey, dataContext: simplifiedLightsViewModel);
                    else
                        CloseWindow(SimplifiedLightsWindowKey);
                }
            }
        }

        public bool IsPartsResultsWindowVisible
        {
            get => windowVisibility[PartsResultsWindowKey];
            set
            {
                if (windowVisibility[PartsResultsWindowKey] != value)
                {
                    windowVisibility[PartsResultsWindowKey] = value;
                    OnPropertyChanged(nameof(IsPartsResultsWindowVisible));
                    if (value)
                        ShowWindow<PartsResultsWindow>(PartsResultsWindowKey, dataContext: partsResultsViewModel);
                    else
                        CloseWindow(PartsResultsWindowKey);
                }
            }
        }

        public bool IsSimplifiedEpiscopicLightWindowVisible
        {
            get => windowVisibility[SimplifiedEpiscopicLightWindowKey];
            set
            {
                if (windowVisibility[SimplifiedEpiscopicLightWindowKey] != value)
                {
                    windowVisibility[SimplifiedEpiscopicLightWindowKey] = value;
                    OnPropertyChanged(nameof(IsSimplifiedEpiscopicLightWindowVisible));
                    if (value)
                        ShowWindow<SimplifiedEpiscopicLightWindow>(SimplifiedEpiscopicLightWindowKey, dataContext: simplifiedEpiscopicLightViewModel);
                    else
                        CloseWindow(SimplifiedEpiscopicLightWindowKey);
                }
            }
        }

        public bool IsSimplifiedDiascopicLightWindowVisible
        {
            get => windowVisibility[SimplifiedDiascopicLightWindowKey];
            set
            {
                if (windowVisibility[SimplifiedDiascopicLightWindowKey] != value)
                {
                    windowVisibility[SimplifiedDiascopicLightWindowKey] = value;
                    OnPropertyChanged(nameof(IsSimplifiedDiascopicLightWindowVisible));
                    if (value)
                        ShowWindow<SimplifiedDiascopicLightWindow>(SimplifiedDiascopicLightWindowKey, dataContext: simplifiedDiascopicLightViewModel);
                    else
                        CloseWindow(SimplifiedDiascopicLightWindowKey);
                }
            }
        }

        public bool IsSimplifiedAuxiliaryLightWindowVisible
        {
            get => windowVisibility[SimplifiedAuxiliaryLightWindowKey];
            set
            {
                if (windowVisibility[SimplifiedAuxiliaryLightWindowKey] != value)
                {
                    windowVisibility[SimplifiedAuxiliaryLightWindowKey] = value;
                    OnPropertyChanged(nameof(SimplifiedAuxiliaryLightWindowKey));
                    if (value)
                        ShowWindow<SimplifiedAuxiliaryLightWindow>(SimplifiedAuxiliaryLightWindowKey, dataContext: simplifiedAuxiliaryLightViewModel);
                    else
                        CloseWindow(SimplifiedAuxiliaryLightWindowKey);
                }
            }
        }
        #endregion

        public MainWindowViewModel()
        {

            openWindows = new Dictionary<string, Window>(); // Initialisation du dictionnaire des fenêtres

            #region Initialisation des ViewModels et des COmmands associées
            Commands = new Dictionary<string, RelayCommand>
            {
                #region Modes de fonctionnement (en haut à gauche)

                #endregion
                #region Actions de l'interface (en haut à droite)
                { "MinimizeApplicationCommand", new RelayCommand(MinimizeApplication)},
                { "ExitApplicationCommand", new RelayCommand(ExitApplication)},
                #endregion
                #region Affichage des fenêtres (à gauche)
                { "ToggleSimplifiedLightsViewCommand", new RelayCommand(ToggleSimplifiedLightsView)},
                { "TogglePartsResultsViewCommand", new RelayCommand(TogglePartsResultsView)},
                { "ToggleSimplifiedEpiscopicLightViewCommand", new RelayCommand(ToggleSimplifiedEpiscopicLightView)},
                #endregion
                #region Commandes machines (à droite)

                #endregion
            };

            simplifiedLightsViewModel = new SimplifiedLightsViewModel()
            {
                Commands = new Dictionary<string, RelayCommand>
                {
                    { "ToggleSimplifiedLightsViewCommand", new RelayCommand(ToggleSimplifiedLightsView)},
                    { "ToggleSimplifiedEpiscopicLightViewCommand", new RelayCommand(ToggleSimplifiedEpiscopicLightView)},
                    { "ToggleSimplifiedDiascopicLightViewCommand", new RelayCommand(ToggleSimplifiedDiascopicLightView)},
                    { "ToggleSimplifiedAuxiliaryLightViewCommand", new RelayCommand(ToggleSimplifiedAuxiliaryLightView)},
                }
            };

            simplifiedEpiscopicLightViewModel = new SimplifiedEpiscopicLightViewModel();
            simplifiedDiascopicLightViewModel = new SimplifiedDiascopicLightViewModel();
            simplifiedAuxiliaryLightViewModel = new SimplifiedAuxiliaryLightViewModel();

            partsResultsViewModel = new PartsResultsViewModel();
            #endregion


            #region  Associations de la définition des ICommand et du code source associée

            AddMeasurementCommand_DEV = new RelayCommand(AddMeasurement_DEV);
            #endregion

            #region Test éclairage
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
            #endregion

            Messenger.Instance.Subscribe<SliderValueChangedMessage>(OnMessage_SliderValueChanged);
        }

        private void OnMessage_SliderValueChanged(SliderValueChangedMessage message)
        {
            // Logique pour gérer la modification de la valeur du slider
            Console.WriteLine($"Nouvelle valeur du slider en provenance de {message.From.ToString()} : {message.NewValue}");
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

        #region Fonctions relatives à la gestion de l'affichage des différentes fenêtres 
        private void ShowWindow<T>(string windowKey, object dataContext = null, bool forceDataContext = false, Point? position = null) where T : Window, new()
        {
            if (openWindows.ContainsKey(windowKey))
            {
                var window = openWindows[windowKey];
                window.Show();
                window.Activate();
            }
            else
            {
                T window = new T();
                if (Application.Current.MainWindow != null && window != Application.Current.MainWindow)
                {
                    window.Owner = Application.Current.MainWindow;
                }
                window.Closed += (s, args) =>
                {
                    openWindows.Remove(windowKey);
                };
                openWindows[windowKey] = window;
                if (window is ILateInitializableDataContextWindow lateInitializableDataContextWindow)
                {
                    lateInitializableDataContextWindow.InitializeDataContext(dataContext);
                }
                else
                {
                    if (window.DataContext == null || forceDataContext)
                    {
                        window.DataContext = dataContext;
                    }
                }
                if (position.HasValue)
                {
                    window.Left = position.Value.X;
                    window.Top = position.Value.Y;
                }
                window.Show();
            }
        }

        private void ToggleWindowVisibility(string windowKey, Point? position = null)
        {
            switch (windowKey)
            {
                case SimplifiedLightsWindowKey:
                    IsSimplifiedLightsWindowVisible = !windowVisibility[windowKey];
                    break;
                case PartsResultsWindowKey:
                    IsPartsResultsWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedEpiscopicLightWindowKey:
                    IsSimplifiedEpiscopicLightWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedDiascopicLightWindowKey:
                    IsSimplifiedDiascopicLightWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedAuxiliaryLightWindowKey:
                    IsSimplifiedAuxiliaryLightWindowVisible = !windowVisibility[windowKey];
                    break;
            }
        }

        private void CloseWindow(string windowKey)
        {
            if (openWindows.ContainsKey(windowKey))
            {
                openWindows[windowKey].Close();
            }
        }

        private void ToggleWindowVisibility(string windowKey)
        {
            switch (windowKey)
            {
                case SimplifiedLightsWindowKey:
                    IsSimplifiedLightsWindowVisible = !windowVisibility[windowKey];
                    if (!IsSimplifiedLightsWindowVisible)
                    {
                        Messenger.Instance.Send(new WindowClosedMessage(windowKey));
                    }
                    break;
                case PartsResultsWindowKey:
                    IsPartsResultsWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedEpiscopicLightWindowKey:
                    IsSimplifiedEpiscopicLightWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedDiascopicLightWindowKey:
                    IsSimplifiedDiascopicLightWindowVisible = !windowVisibility[windowKey];
                    break;
                case SimplifiedAuxiliaryLightWindowKey:
                    IsSimplifiedAuxiliaryLightWindowVisible = !windowVisibility[windowKey];
                    break;
                    // Ajoutez des cas pour d'autres fenêtres ici
            }
        }

        private void ToggleSimplifiedLightsView()
        {
            ToggleWindowVisibility(SimplifiedLightsWindowKey);
        }

        private void TogglePartsResultsView()
        {
            ToggleWindowVisibility(PartsResultsWindowKey);
        }

        public void ToggleSimplifiedEpiscopicLightView(Point position)
        {
            if (IsSimplifiedEpiscopicLightWindowVisible)
            {
                CloseWindow(SimplifiedEpiscopicLightWindowKey);
            }
            else
            {
                ShowWindow<SimplifiedEpiscopicLightWindow>(SimplifiedEpiscopicLightWindowKey, dataContext: simplifiedEpiscopicLightViewModel, position: position);
            }
            IsSimplifiedEpiscopicLightWindowVisible = !IsSimplifiedEpiscopicLightWindowVisible;
        }

        private void ToggleSimplifiedEpiscopicLightView()
        {
            ToggleWindowVisibility(SimplifiedEpiscopicLightWindowKey);
        }

        private void ToggleSimplifiedDiascopicLightView()
        {
            ToggleWindowVisibility(SimplifiedDiascopicLightWindowKey);
        }

        private void ToggleSimplifiedAuxiliaryLightView()
        {
            ToggleWindowVisibility(SimplifiedAuxiliaryLightWindowKey);
        }
        #endregion

        #region Fonctions de Debug en DEV
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
        #endregion
    }
}
