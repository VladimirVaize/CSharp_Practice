using System;
using System.Collections.Generic;

namespace PatternFacade.Subsystems
{
    public class GameLogger
    {
        private List<string> _logs = new List<string>();

        public void Initialize()
        {
            Console.WriteLine("[Logger] Инициализация логгера...");
            Log($"--- Игра запущена {DateTime.Now} ---");
        }

        public void Log(string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}";
            _logs.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void Shutdown()
        {
            Console.WriteLine("[Logger] Завершение работы логгера...");
        }
    }
}
