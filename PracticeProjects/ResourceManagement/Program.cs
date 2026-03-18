using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace ResourceManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Тест 1: Корректное использование ===");
            using (var session = new GameSession())
            {
                session.Run();
            }

            Console.WriteLine("\n=== Тест 2: Проверка автосохранения ===");
            if (File.Exists("saves/autosave.sav"))
                Console.WriteLine("Файл сохранения создан");
            if (File.Exists("session_log.txt"))
                Console.WriteLine("Файл лога создан");

            Console.WriteLine("\n=== Тест 3: Обработка ошибок ===");
            try
            {
                var badSaver = new GameSaver(new GameState());
                badSaver.SaveToFile("Z:\\несуществующая_папка\\save.sav");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка перехвачена: {ex.Message}");
            }

            Console.WriteLine("\n=== Тест 4: Забытый логгер (сработает финализатор) ===");
            CreateLoggerWithoutDispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("Программа завершена. Проверь файлы лога.");
        }

        static void CreateLoggerWithoutDispose()
        {
            var logger = new GameLogger();
            logger.Log("Это сообщение может не сохраниться!");
        }
    }

    public class GameLogger : IDisposable
    {
        private StreamWriter _writer;
        private bool _disposed = false;

        public GameLogger()
        {
            _writer = new StreamWriter("session_log.txt", true);
            _writer.AutoFlush = true;
        }

        public void Log(string message)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(GameLogger));
            _writer?.WriteLine($"[{DateTime.Now}] {message}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writer?.Dispose();
                }
                _disposed = true;
            }
        }

        ~GameLogger()
        {
            Dispose(false);
            Console.WriteLine("[!] Логгер не был явно закрыт! Данные могли потеряться.");
        }
    }

    public class GameSaver
    {
        private GameState _gameState;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IncludeFields = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public GameSaver(GameState gameState)
        {
            _gameState = gameState;
        }

        public void SaveToFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            string tempFile = filePath + ".tmp";
            string directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            try
            {
                using (var writer = new StreamWriter(tempFile))
                {
                    string json = JsonSerializer.Serialize(_gameState, _jsonOptions);
                    writer.Write(json);
                }

                if (File.Exists(filePath))
                    File.Delete(filePath);

                File.Move(tempFile, filePath);

                Console.WriteLine($"[Сохранение] Игра сохранена в {filePath}");
            }
            catch (Exception ex)
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);

                Console.WriteLine($"[Ошибка сохранения] {ex.Message}");
                throw;
            }
        }
    }

    public class GameState
    {
        public int Level;
        public int Health;
        public float PositionX;
        public float PositionY;
        public List<string> Inventory;

        public GameState()
        {
            Inventory = new List<string>();
        }

        public void Update()
        {
            Health += 10;
            Level++;
            PositionX += 1.5f;
            if (Inventory.Count == 0)
                Inventory.Add("Меч");
            else
                Inventory.Add($"Зелье #{Inventory.Count}");
        }
    }

    public class GameSession : IDisposable
    {
        private GameLogger _logger;
        private GameState _state;
        private GameSaver _saver;
        private bool _disposed = false;

        public GameSession()
        {
            _logger = new GameLogger();
            _state = new GameState();
            _saver = new GameSaver(_state);

            _logger.Log("Сессия начата");
        }

        public void Run()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    _state.Update();
                    _logger.Log($"Шаг {i}: Здоровье {_state.Health}, Позиция ({_state.PositionX}, {_state.PositionY})");

                    if (i % 3 == 0)
                    {
                        _saver.SaveToFile("saves/autosave.sav");
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Ошибка в сессии: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _logger?.Log("Сессия завершена");
                _logger?.Dispose();
                _disposed = true;
            }
        }
    }
}
