using System.Collections.Generic;
using System.Threading.Tasks;
using SlapCheckersLib;

namespace SlapCheckersClient
{
    internal interface IClient
    {
        /// <summary>
        /// Connects to the server.
        /// </summary>
        void Connect(string serverIpAddress, int serverPort);
    }
}