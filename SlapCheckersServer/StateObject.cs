using System.Net.Sockets;
using System.Text;

namespace SlapCheckersServer
{
    internal class StateObject
    {
        public Socket Socket { get; set; }

        public const int BufferSize = 1024;

        public byte[] Buffer = new byte[BufferSize];

        public StringBuilder sb { get; set; }
    }
}