using LightingBoards.Interfaces;
using System;

namespace LightingBoards.Connections
{
    public static class LightingBoardConnectionFactory
    {
        public static ILightingBoardConnection CreateConnection(string connectionType, string addressOrPort, int port = 0)
        {
            switch (connectionType)
            {
                case "TCP":
                    return new LightingBoard_TCPConnection(addressOrPort, port);
                case "COM":
                    return new LightingBoard_ComConnection(addressOrPort);
                default:
                    throw new ArgumentException("Invalid connection type");
            }
        }
    }
}
