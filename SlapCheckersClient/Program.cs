using System;
using Serilog;

namespace SlapCheckersClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogger();

            Client client = new Client();
            client.Connect("127.0.0.1", 27015);
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
