using System;

using NotKyle.WebServer;
using NotKyle.Logger;

namespace ConsoleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Start();
            Logger.Log("Server started on port 80");
            Console.ReadLine();

            Logger.Log("Server stopped");
        }
    }
}

