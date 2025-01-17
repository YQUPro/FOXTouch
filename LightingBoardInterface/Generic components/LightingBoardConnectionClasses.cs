using LightingBoards.Interfaces;
using System;

namespace LightingBoards.Connections
{
    public class LightingBoard_TCPConnection : ILightingBoardConnection
    {
        public string IPAddress { get; }
        public int Port { get; }

        public LightingBoard_TCPConnection(string ipAddress, int port)
        {
            IPAddress = ipAddress;
            Port = port;
        }

        ILightingBoardConnection ILightingBoardConnection.GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }

    public class LightingBoard_ComConnection : ILightingBoardConnection
    {
        public string PortName { get; }

        public LightingBoard_ComConnection(string portName)
        {
            PortName = portName;
        }

        ILightingBoardConnection ILightingBoardConnection.GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
