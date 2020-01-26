using Serilog;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using SlapCheckersLib.Requests;

namespace SlapCheckersServer
{
    internal class Server
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private TcpListener _listener;
        private List<StateObject> _connections;

        public Server(string ipAddress, int port)
        {
            _connections = new List<StateObject>();

            _ipAddress = IPAddress.Parse(ipAddress);
            _port = port;
        }
        
        public void Start()
        {
            StartListening();
        }

        private void StartListening() 
        {
            Task.Run(() => 
            {
                IPEndPoint serverEndPoint = new IPEndPoint(_ipAddress, _port);
                _listener = new TcpListener(serverEndPoint);
                _listener.Start();
                Console.Write("   ");
                for (int i = 0; i<8; i++) {
                    Console.Write($"{(char)(65 + i)}  ");
                }
                Console.WriteLine();
                for (double i = 0; i<8; i++) {
                    for (int j = 0; j<9; j++) {
                        if (j == 0) {
                            Console.BackgroundColor = default;
                            Console.Write($"{i + 1} ");
                            continue;
                        }
                        Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                        Console.ForegroundColor =  ConsoleColor.Red;
                        Console.Write(" ● ");
                    }
                    Console.BackgroundColor = default;
                    Console.WriteLine();
                }
                Log.Information($"Server started successfully on {serverEndPoint}...");
                _listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), null);
            });

            // Random rnd = new Random();
            // Task.Run(async () => 
            // {
            //     while(true)
            //     {
            //         _connections.ForEach(c => 
            //         {
            //             int command = rnd.Next(0, 10);
            //             byte[] commandByte = BitConverter.GetBytes(command);
            //             c.Socket.BeginSend(BitConverter.GetBytes(command), 0, commandByte.Length, 0, new AsyncCallback(SendCallback), c);
            //         });
            //         await Task.Delay(1000);
            //     }
            // });
        }

        #region Listener

        private void AcceptCallback(IAsyncResult ar) 
        {
            Socket handler = _listener.EndAcceptSocket(ar);

            Log.Information($"{handler.RemoteEndPoint} connected.");

            StateObject state = new StateObject
            {
                Socket = handler
            };
            
            _connections.Add(state);
            Log.Information($"Number of active connections: {_connections.Count}.");
            
            handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

            if (_connections.Count < 2)
            {
                _listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), null);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.Socket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                var request = Request.Unpack(state.Buffer);
                Log.Debug($"Received {request.Type} request.");
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject) ar.AsyncState;
            state.Socket.EndSend(ar);
        }

        #endregion
    }
}