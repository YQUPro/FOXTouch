//*************************************************************************************************************************************
// 20241210 YQU V1.0.0
// Cette classe permet de stocker l'ensemble des valeurs mesurées pour l'ensemble des caractéristiques d'un lot.
// Cet objet sert de source d'informations à la vue PartResultViewModel (affichage des valeurs mesurées de la dernière pièce).
// Cet objet doit être alimenté par les résultats provenant dse objets réalisant les différentées mesures attendues
//*************************************************************************************************************************************

using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;
using static FOXTouchEnumerations.FeaturesType.FeatureTypeEnumerations;
using static FOXTouchEnumerations.MeasurementResults.MeasurementResultsEnumerations;
using static FOXTouchEnumerations.Tolerancing.TolerancingModeEnumerations;

namespace FOXTouch_WPF.ViewModels
{
    public class MeasurementValueListingItemViewModel : ViewModelBase
    {
        public string Name { get; }
        public EResultFeatureType ResultFeatureType { get; }
        public ObservableCollection<string> Values { get; }
        public float NominalValue { get; }
        public string Unit { get; }
        public ETolerancingMode ToleranceMode { get; }
        public float UpperTolerance { get; }
        public float LowerTolerance { get; }
        public string LastStoredValue => Values.LastOrDefault();

        public string LastStoredValueDeviation
        {
            get
            {
                string stringDeviation = "";

                if (LastStoredValueMeasurementResult != EMeasurementResult.NotMeasured)
                {
                    if (ToleranceMode != ETolerancingMode.NoTolerancing)
                    {
                        // Valeur hors de l'IT
                        if (GetFloatFromString(LastStoredValue) < NominalValue + LowerTolerance)
                            stringDeviation = @"<----";
                        else if (GetFloatFromString(LastStoredValue) > NominalValue + UpperTolerance)
                            stringDeviation = @"---->";

                        //Calcul de la situation, en pourcentage, si la valeur est l'intervalle de tolérances
                        float percentageSituation = ((GetFloatFromString(LastStoredValue) - (NominalValue + LowerTolerance)) / (UpperTolerance - LowerTolerance)) * 100;
                        if (percentageSituation >= 0 && percentageSituation < 20)
                            stringDeviation = @"+----";
                        else if (percentageSituation >= 20 && percentageSituation < 40)
                            stringDeviation = @"-+---";
                        else if (percentageSituation >= 40 && percentageSituation < 60)
                            stringDeviation = @"--+--";
                        else if (percentageSituation >= 60 && percentageSituation < 80)
                            stringDeviation = @"---+-";
                        else if (percentageSituation >= 80 && percentageSituation <= 100)
                            stringDeviation = @"----+";
                    }
                }
                return stringDeviation;
            }
        }

        public BitmapImage PictureLastStoredValueMeasurementResult
        {
            get
            {
                switch (LastStoredValueMeasurementResult)
                {
                    case EMeasurementResult.NotToleranced:
                        return ConvertBitmapToBitmapImage(Properties.Resources.LEDIndicator_Purple);
                    case EMeasurementResult.InTolerances:
                        return ConvertBitmapToBitmapImage(Properties.Resources.LEDIndicator_Green);
                    case EMeasurementResult.OutOfTolerances:
                        return ConvertBitmapToBitmapImage(Properties.Resources.LEDIndicator_Red);
                    case EMeasurementResult.NotMeasured:
                        return ConvertBitmapToBitmapImage(Properties.Resources.LEDIndicator_Black);
                    default:
                        return ConvertBitmapToBitmapImage(Properties.Resources.LEDIndicator_Blue); ;
                }
            }
        }

        public EMeasurementResult LastStoredValueMeasurementResult
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LastStoredValue))
                    return EMeasurementResult.NotMeasured;

                if (ToleranceMode == ETolerancingMode.NoTolerancing)
                    return EMeasurementResult.NotToleranced;
                else
                {
                    if ((Convert.ToSingle(LastStoredValue) > NominalValue + LowerTolerance) && (Convert.ToSingle(LastStoredValue) < NominalValue + UpperTolerance))
                        return EMeasurementResult.InTolerances;
                    else
                        return EMeasurementResult.OutOfTolerances;
                }
            }
        }

        public BitmapImage PictureResultFeatureType
        {
            get
            {
                switch (ResultFeatureType)
                {
                    case EResultFeatureType.Undefined:
                        return ConvertBitmapToBitmapImage(Properties.Resources.PictureResultType_UndefinedOrDefault);
                    case EResultFeatureType.Type01:
                        return ConvertBitmapToBitmapImage(Properties.Resources.PictureResultType_Type01);
                    case EResultFeatureType.Type02:
                        return ConvertBitmapToBitmapImage(Properties.Resources.PictureResultType_Type02);
                    case EResultFeatureType.Type03:
                        return ConvertBitmapToBitmapImage(Properties.Resources.PictureResultType_Type03);
                    default:
                        return ConvertBitmapToBitmapImage(Properties.Resources.PictureResultType_UndefinedOrDefault);
                }
            }
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(Values));
            OnPropertyChanged(nameof(LastStoredValue));
            OnPropertyChanged(nameof(LastStoredValueDeviation));
            OnPropertyChanged(nameof(LastStoredValueMeasurementResult));
            OnPropertyChanged(nameof(PictureLastStoredValueMeasurementResult));
        }

        public MeasurementValueListingItemViewModel(string name, List<string> values, string unit, float nominalValue, ETolerancingMode toleranceMode, float upperTolerance, float lowerTolerance, EResultFeatureType resultFeatureType)
        {
            Name = name;
            Values = new ObservableCollection<string>(values);
            Unit = unit;
            NominalValue = nominalValue;
            ToleranceMode = toleranceMode;
            ResultFeatureType = resultFeatureType;

            switch (ToleranceMode)
            {
                case ETolerancingMode.NoTolerancing:
                    UpperTolerance = 0f;
                    LowerTolerance = 0f;
                    break;

                case ETolerancingMode.DoubleTolerancing:
                    UpperTolerance = upperTolerance;
                    LowerTolerance = lowerTolerance;
                    break;

                case ETolerancingMode.CenteredTolerancing:
                    UpperTolerance = upperTolerance;
                    LowerTolerance = -upperTolerance;
                    break;

                case ETolerancingMode.SimpleUpperTolerancing:
                    UpperTolerance = upperTolerance;
                    LowerTolerance = 0f;
                    break;

                case ETolerancingMode.SimpleLowerTolerancing:
                    UpperTolerance = 0f;
                    LowerTolerance = lowerTolerance;
                    break;

                default:
                    UpperTolerance = 0f;
                    LowerTolerance = 0f;
                    break;
            }
        }

        public MeasurementValueListingItemViewModel(MeasurementValueListingItemViewModel unitaryMeasurement)
        {
            Name = unitaryMeasurement.Name;
            Values = unitaryMeasurement.Values;
            Unit = unitaryMeasurement.Unit;
            NominalValue = unitaryMeasurement.NominalValue;
            ToleranceMode = unitaryMeasurement.ToleranceMode;
            UpperTolerance = unitaryMeasurement.UpperTolerance;
            LowerTolerance = unitaryMeasurement.LowerTolerance;
        }

        public EFunctionErrorCode AddMeasurementValue(string newValue)
        {
            Values.Add(newValue);
            UpdateProperties();
            return EFunctionErrorCode.Success;
        }

        public EFunctionErrorCode AddMeasurementValue(float newValue)
        {
            Values.Add((newValue).ToString());
            UpdateProperties();
            return EFunctionErrorCode.Success;
        }

        public EFunctionErrorCode RemoveMeasurementValue(int index)
        {
            if (index > Values.Count)
                return EFunctionErrorCode.Failed;

            Values.RemoveAt(index);
            UpdateProperties();
            return EFunctionErrorCode.Success;
        }

        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmapToConvert)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the System.Drawing.Image to the MemoryStream
                bitmapToConvert.Save(memoryStream, bitmapToConvert.RawFormat);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Create a new BitmapImage and load the MemoryStream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        

        private float GetFloatFromString(string value)
        {
            return Convert.ToSingle(value);
        }
    }
}
