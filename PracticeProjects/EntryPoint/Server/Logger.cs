using System;

namespace EntryPoint.Server
{
    public static class Logger
    {
        public static event LogHandler OnLog;

        public static void Log(string message)
        {
            var timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            OnLog?.Invoke(timestampedMessage);
            Console.WriteLine(timestampedMessage);
        }
    }

    public delegate void LogHandler(string message);
}
