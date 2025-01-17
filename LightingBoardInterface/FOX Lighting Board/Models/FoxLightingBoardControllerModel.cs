using LightingBoards.Connections;
using LightingBoards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TCP_UDP_NetworkAPI;
using TimeStampedData;
using static FOXTouchEnumerations.ErrorCodes.FunctionErrorCodeEnumerations;
using static FOXTouchLightingBoards.FoxLightingBoardEnumerations;

namespace FOXTouchLightingBoards
{
    public class FoxLightingBoardControllerModel : ILightingBoardClass
    {
        #region Members
        private List<TimeStampedByte> _linesValues = new List<TimeStampedByte> {
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue),
            new TimeStampedByte(byte.MinValue)
        };
        LightingBoard_TCPConnection _connection;
        private TimeStampedBoolean _isTCPMessageTransferedToParent = new TimeStampedBoolean();
        #endregion

        #region Properties
        public static char[] TCPDatagramSeparators = { ';', '#' };

        public byte L0_ActualValue
        {
            get => _linesValues[0].Value;
            set
            {
                if (_linesValues[0].Value != value)
                {
                    _linesValues[0].Value = value;
                }
            }
        }
        public byte L1_ActualValue
        {
            get => _linesValues[1].Value;
            set
            {
                if (_linesValues[1].Value != value)
                {
                    _linesValues[1].Value = value;
                }
            }
        }
        public byte L2_ActualValue
        {
            get => _linesValues[2].Value;
            set
            {
                if (_linesValues[2].Value != value)
                {
                    _linesValues[2].Value = value;
                }
            }
        }
        public byte L3_ActualValue
        {
            get => _linesValues[3].Value;
            set
            {
                if (_linesValues[3].Value != value)
                {
                    _linesValues[3].Value = value;
                }
            }
        }
        public byte L4_ActualValue
        {
            get => _linesValues[4].Value;
            set
            {
                if (_linesValues[4].Value != value)
                {
                    _linesValues[4].Value = value;
                }
            }
        }
        public byte L5_ActualValue
        {
            get => _linesValues[5].Value;
            set
            {
                if (_linesValues[5].Value != value)
                {
                    _linesValues[5].Value = value;
                }
            }
        }
        public byte L6_ActualValue
        {
            get => _linesValues[6].Value;
            set
            {
                if (_linesValues[6].Value != value)
                {
                    _linesValues[6].Value = value;
                }
            }
        }
        public byte L7_ActualValue
        {
            get => _linesValues[7].Value;
            set
            {
                if (_linesValues[7].Value != value)
                {
                    _linesValues[7].Value = value;
                }
            }
        }

        #endregion

        #region Events
        public event Action<string> TCPMessageReceived;
        public event Action linesValuesChanged;
        #endregion

        #region Constructor
        public FoxLightingBoardControllerModel(LightingBoard_TCPConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Functions

        private EFunctionErrorCode TCPConnect(string ipAddress, int port, bool transferTCPMessageToParent = false)
        {
            if (!IPAddress.TryParse(ipAddress.ToString(), out IPAddress tmp))
                return EFunctionErrorCode.Failed;
            //throw new Exception("Adresse IP saisie non coforme. Veuillez corriger l'adresse IP avant de relancer la connexion.");

            _isTCPMessageTransferedToParent.Value = transferTCPMessageToParent;
            TCPUDPNetworkAPI.TcpNetworkConnectionAccepted connectionDelegate = new TCPUDPNetworkAPI.TcpNetworkConnectionAccepted(tcpConnectionAccepted);
            try
            {
                TCPUDPNetworkAPI.Connect(connectionDelegate, tmp.ToString(), port);
                return EFunctionErrorCode.Success;
            }
            catch (Exception ex)
            {
                return EFunctionErrorCode.Failed;
                // throw ex;
            }
        }

        private void tcpConnectionAccepted(bool success, EndPoint from)
        {

            if (success)
            {
                TCPUDPNetworkAPI.NetworkMessageReceived msgDelegate = new TCPUDPNetworkAPI.NetworkMessageReceived(tcpMessageReceived);
                TCPUDPNetworkAPI.Receive(msgDelegate, (TCPUDPNetworkAPI.SocketEncoding)Enum.Parse(typeof(TCPUDPNetworkAPI.SocketEncoding), "ASCII"));
            }
            else
            {
                //TCPConnectionStatus = TCPConnectionState.Disconnected;
                //Connect or Listen failed
                //UpdateTCPUIElements(true, false);
                //lblRemoteControllerEndPoint.ResetText();
            }

        }

        /// <summary>
        /// Fonction de réception des messages bruts. Ces messages sont transmis à la fonction ManageTCPDatagram pour interprétation
        /// </summary>
        /// <param name="from">Contient l'adresse IP et le port de provenance du message</param>
        /// <param name="msg">Contient le message transmis</param>
        private void tcpMessageReceived(string from, string msg)
        {
            if (from != null)
            {
                ManageTCPDatagram(msg);
                if (_isTCPMessageTransferedToParent.Value)
                {
                    TCPMessageReceived?.Invoke(msg);
                }
                TCPUDPNetworkAPI.NetworkMessageReceived msgDelegate = new TCPUDPNetworkAPI.NetworkMessageReceived(tcpMessageReceived);
                TCPUDPNetworkAPI.Receive(msgDelegate, (TCPUDPNetworkAPI.SocketEncoding)Enum.Parse(typeof(TCPUDPNetworkAPI.SocketEncoding), "ASCII"));
            }
            else
            {
                TCPUDPNetworkAPI.Close();
            }
        }

        /// <summary>
        /// Fonction appelée afin de prendre en charge le décodage des trames reçues et met à jour les éléments nécessaires pour une réponse
        /// </summary>
        /// <param name="tcpDatagram"></param>
        private int ManageTCPDatagram(string tcpDatagram)
        {
            if (Helpers.expectedReplyPattern.IsMatch(tcpDatagram))
            {

                List<string> splittedMessage = tcpDatagram.Split(TCPDatagramSeparators).ToList();
                //Remove the last element that contains Checksum
                splittedMessage.RemoveAt(splittedMessage.Count() - 1);
                //Remove the first element that contains Address
                splittedMessage.RemoveAt(0);

                if (splittedMessage.Count != _linesValues.Capacity)
                    return (int)EUpdateLinesFunctionErrorCode.Reply_MismatchWithExpectedValues;

                for (int i = 0; i < splittedMessage.Count; i++)
                {
                    _linesValues[i].Value = Convert.ToByte(splittedMessage[i].Substring(splittedMessage[i].Length - 2, 2), 16);
                }

                linesValuesChanged?.Invoke();
                return (int)EUpdateLinesFunctionErrorCode.OK;
            }
            else if (Helpers.AddressErrorReplyPattern.IsMatch(tcpDatagram))
            {
                return (int)EUpdateLinesFunctionErrorCode.Protocol_AddressError;
            }
            else if (Helpers.ChecksumErrorReplyPattern.IsMatch(tcpDatagram))
            {
                return (int)EUpdateLinesFunctionErrorCode.Protocol_ChecksumError;
            }
            else if (Helpers.ParamErrorReplyPattern.IsMatch(tcpDatagram))
            {
                return (int)EUpdateLinesFunctionErrorCode.Protocol_ParamError;
            }

            return (int)EUpdateLinesFunctionErrorCode.ImpossibleToProcess;
        }

        /// <summary>
        /// Fonction d'envoi d'un message à l'aide de la connextion TCP, paramétrée en amont
        /// </summary>
        /// <param name="messageToSend">Datagramme à envoyé au système distant</param>
        private void TCPSend(string messageToSend)
        {
            TCPUDPNetworkAPI.Send(messageToSend, (TCPUDPNetworkAPI.SocketEncoding)Enum.Parse(typeof(TCPUDPNetworkAPI.SocketEncoding), "ASCII"));
        }

        public EFunctionErrorCode SetLinesValues(bool isUsed_Line0, byte value_Line0, bool isUsed_Line1, byte value_Line1, bool isUsed_Line2, byte value_Line2, bool isUsed_Line3, byte value_Line3, bool isUsed_Line4, byte value_Line4, bool isUsed_Line5, byte value_Line5, bool isUsed_Line6, byte value_Line6, bool isUsed_Line7, byte value_Line7)
        {
            StringBuilder datagram = new StringBuilder();
            datagram.Append(
                "&00;"
                + Convert.ToInt32(isUsed_Line0) + (isUsed_Line0 ? Convert.ToInt32(value_Line0).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line1) + (isUsed_Line1 ? Convert.ToInt32(value_Line1).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line2) + (isUsed_Line2 ? Convert.ToInt32(value_Line2).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line3) + (isUsed_Line3 ? Convert.ToInt32(value_Line3).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line4) + (isUsed_Line4 ? Convert.ToInt32(value_Line4).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line5) + (isUsed_Line5 ? Convert.ToInt32(value_Line5).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line6) + (isUsed_Line6 ? Convert.ToInt32(value_Line6).ToString("X2") : "00") + ";"
                + Convert.ToInt32(isUsed_Line7) + (isUsed_Line7 ? Convert.ToInt32(value_Line7).ToString("X2") : "00") + "#");

            datagram.Append(ComputeChecksum(datagram.ToString()));
            datagram.Append('\r');

            TCPSend(datagram.ToString());
            return EFunctionErrorCode.Success;
        }

        private string ComputeChecksum(string sourceString)
        {
            byte chkSum = 0;
            for (int i = 0; i < sourceString.Length; i++)
            {
                chkSum += Encoding.ASCII.GetBytes(sourceString.Substring(i, 1))[0];
            }

            return chkSum.ToString("X2");
        }
        #endregion

        #region Interface implementation
        public EFunctionErrorCode Start()
        {
            if (_connection != null)
            {
                return TCPConnect(_connection.IPAddress, _connection.Port);
            }
            return EFunctionErrorCode.Failed;
        }

        public EFunctionErrorCode UpdatesLights(byte L0, byte L1, byte L2, byte L3, byte L4, byte L5, byte L6, byte L7)
        {
            return SetLinesValues(
                true, L0
                , true, L1
                , true, L2
                , true, L3
                , true, L4
                , true, L5
                , true, L6
                , true, L7
                );
            //throw new NotImplementedException();
        }

        public EFunctionErrorCode Stop()
        {
            TCPUDPNetworkAPI.Close();
            return EFunctionErrorCode.Success;
        }
        #endregion
    }
}
