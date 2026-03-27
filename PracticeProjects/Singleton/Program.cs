using System;
using System.Collections.Generic;

namespace Singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogger.Instance.LogInfo("Игрок загрузился в локации \"Лес\"");
            GameLogger.Instance.LogInfo("Игрок получил 10 урона");
            GameLogger.Instance.LogWarning("У игрока осталось 5 здоровья!");
            GameLogger.Instance.LogInfo("Подобран предмет \"Целебное зелье\"");
            GameLogger.Instance.LogError("Не удалось сохранить игру: файл занят");

            Console.WriteLine("\n=== ВСЕ ЛОГИ ===");
            GameLogger.Instance.PrintAllLogs();
        }
    }

    public class GameLogger
    {
        public static GameLogger Instance { get; } = new GameLogger();
        private List<string> _logs = new List<string>();

        private GameLogger() { }

        private void Log(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string formattedMessage = $"[{time}] {message}";
            Console.WriteLine(formattedMessage);
            _logs.Add(formattedMessage);
        }

        public void LogInfo(string message) => Log($"[INFO] {message}");

        public void LogWarning(string message) => Log($"[WARNING] {message}");

        public void LogError(string message) => Log($"[ERROR] {message}");

        public void PrintAllLogs()
        {
            if (_logs.Count == 0)
            {
                Console.WriteLine("Лог пуст");
                return;
            }

            for (int i = 0; i < _logs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_logs[i]}");
            }
        }
    }
}
