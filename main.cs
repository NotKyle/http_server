using System;

using NotKyle.WebServer;
using NotKyle.Logger;

namespace ConsoleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = false;

            do
            {
                Logger.Log("Server is not running, automatically starting.");

                try
                {
                    Server.Start();
                    running = true;
                    Logger.Log("Server started successfully, press Ctrl+C to stop.");

                    while (running)
                    {
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log($"Failed to start server: {e.Message}");
                    Logger.Log("Press any key to try again...");
                    Console.ReadKey();
                }
            } while (!running);
        }
    }
}

