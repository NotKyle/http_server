using System;

namespace NotKyle.Logger
{
    public static class Logger
    {
        public enum LogLevel
        {
            INFO,
            DEBUG,
            WARNING,
            ERROR
        }

        // Add colors to logging
        public static void Log(string message, LogLevel level = LogLevel.INFO)
        {
            switch (level)
            {
                case LogLevel.INFO:
                    LogWithColor(message, ConsoleColor.Green);
                    break;
                case LogLevel.DEBUG:
                    LogWithColor(message, ConsoleColor.Cyan);
                    break;
                case LogLevel.WARNING:
                    LogWithColor(message, ConsoleColor.Yellow);
                    break;
                case LogLevel.ERROR:
                    LogWithColor(message, ConsoleColor.Red);
                    break;
            }
        }

        public static void LogWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

