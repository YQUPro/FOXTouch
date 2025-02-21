using GenericComponentsMVVM;
using LightingBoards.Connections;
using LightingBoards.Interfaces;
using System.Windows.Input;
using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;

namespace FOXTouchLightingBoards
{
    public class FoxLightingBoardControllerViewModel : ViewModelBase
    {
        private FoxLightingBoardControllerModel _foxLightingBoardControllerModel;
        private ILightingBoardConnection _connection;


        public FoxLightingBoardControllerViewModel()
        {
            _connection = LightingBoardConnectionFactory.CreateConnection("TCP", "192.168.2.1", 1234);

            _foxLightingBoardControllerModel = new FoxLightingBoardControllerModel(_connection as LightingBoard_TCPConnection) ;
            UpdateLightsValuesCommand = new RelayCommandWithResult<bool, byte, bool, byte, bool, byte, bool, byte, bool, byte, bool, byte, bool, byte, bool, byte, EFunctionErrorCode>(UpdateLightsValues);

            StartLightingBoardCommand = new RelayCommand(StartLightingBoard);
            StopLightingBoardCommand = new RelayCommand(StopLightingBoard);
        }

        #region Définition des commandes
        //public ICommand ConnectCommand { get; }
        public ICommand UpdateLightsValuesCommand { get; }

        public ICommand StartLightingBoardCommand { get; }
        public ICommand StopLightingBoardCommand { get; }
        #endregion  

        #region  Définition des fonctions associées aux commandes
        private EFunctionErrorCode UpdateLightsValues(bool isUsed_Line0, byte value_Line0, bool isUsed_Line1, byte value_Line1, bool isUsed_Line2, byte value_Line2, bool isUsed_Line3, byte value_Line3, bool isUsed_Line4, byte value_Line4, bool isUsed_Line5, byte value_Line5, bool isUsed_Line6, byte value_Line6, bool isUsed_Line7, byte value_Line7)
        {
            return _foxLightingBoardControllerModel.SetLinesValues(isUsed_Line0, value_Line0, isUsed_Line1, value_Line1, isUsed_Line2, value_Line2, isUsed_Line3, value_Line3, isUsed_Line4, value_Line4, isUsed_Line5, value_Line5, isUsed_Line6, value_Line6, isUsed_Line7, value_Line7);
        }

        public void StartLightingBoard()
        {
            var result = _foxLightingBoardControllerModel.Start();
            // Gérer le résultat
        }

        public void StopLightingBoard()
        {
            var result = _foxLightingBoardControllerModel.Stop();
            // Gérer le résultat
        }
        #endregion
    }
}
