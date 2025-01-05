using System;

internal static class NewStaticClass
{

    // Add colors to logging
    public static void Log(string message, LogLevel level = LogLevel.INFO)
    {
        switch (level)
        {
            case LogLevel.INFO:
                LogWithColor(message, ConsoleColor.White);
                break;
            case LogLevel.WARNING:
                LogWithColor(message, ConsoleColor.Yellow);
                break;
            case LogLevel.ERROR:
                LogWithColor(message, ConsoleColor.Red);
                break;
        }
    }
}
