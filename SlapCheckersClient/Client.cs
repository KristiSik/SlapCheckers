using Serilog;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using SlapCheckersLib;

namespace SlapCheckersClient
{
    internal class Client : IClient
    {
        private const int Port = 20800;
        private TcpClient _tcpClient;
        private const int BufferSize = 1024;
        private byte[] _buffer = new byte[BufferSize];

        public Client()
        {
        }

        public void Connect(string serverIpAddress, int serverPort) 
        {
            try 
            {
                _tcpClient = new TcpClient();
                _tcpClient.BeginConnect(serverIpAddress, serverPort, new AsyncCallback(RequestCallback), null);
            } catch (Exception ex)
            {
                Log.Error("Failed to connect to the server. {reason}", ex.Message);
            }

        }

        private void RequestCallback(IAsyncResult ar)
        {
            _tcpClient.EndConnect(ar);

            Log.Information("Successfully connected to the server.");

            _tcpClient.GetStream().BeginRead(_buffer, 0, BufferSize, new AsyncCallback(ReadCallback), null);

        }

        private void ReadCallback(IAsyncResult ar)
        {
            int bytesRead = _tcpClient.GetStream().EndRead(ar);
            Log.Information($"Received data: {Encoding.UTF8.GetString(_buffer)}.");

            if (bytesRead > 0) {
                byte[] message = new byte[bytesRead];
                Array.Copy(_buffer, message, bytesRead);
                int command = BitConverter.ToInt32(message);
                Log.Information($"Parsed command: {command}");
            }

            if (_tcpClient.Connected)
            {
                _tcpClient.GetStream().BeginRead(_buffer, 0, BufferSize, new AsyncCallback(ReadCallback), null);
            } else
            {
                Log.Warning("Connection to the server lost.");
            }
        }

        public IList<Room> GetAvailableRooms()
        {
            return null;
        }
    }
}