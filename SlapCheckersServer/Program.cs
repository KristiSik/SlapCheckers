using System;
using Serilog;

namespace SlapCheckersServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogger();

            Server server = new Server("127.0.0.1", 27015);
            server.Start();
            Console.ReadLine();
        }

        static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Verbose()
                                .WriteTo.Console()
                                .CreateLogger();
        }
    }
}
