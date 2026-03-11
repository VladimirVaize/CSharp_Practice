using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAndAwait
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game();
            await game.LoadLevel("Подземелье");
        }
    }

    public struct LevelData
    {
        public string LevelName { get; }
        public int Width { get; }
        public int Height { get; }
        public List<string> Enemies { get; }
        public Dictionary<string, int> Resources { get; }

        public LevelData(string levelName, int width, int height, List<string> enemies, Dictionary<string, int> resources)
        {
            LevelName = levelName;
            Width = width;
            Height = height;
            Enemies = enemies;
            Resources = resources;
        }
    }

    public class LevelLoader
    {
        private Random _random = new Random();

        public async Task<LevelData> LoadLevelAsync(string levelName)
        {
            for (int i = 0; i <= 100; i += 5)
            {
                await Task.Delay(600);
            }

            List<string> enemies = new List<string> { "Гоблин", "Тролль", "Паук" };

            Dictionary<string, int> resources = new Dictionary<string, int>
            {
                ["Золото"] = 100,
                ["Железо"] = 250,
                ["Зелье"] = 5,
                ["Сундук"] = 12
            };

            LevelData levelData = new LevelData(levelName, 15, 30, enemies, resources);

            if (_random.NextDouble() <= 0.2)
            {
                throw new InvalidOperationException($"Файл уровня '{levelName}' поврежден");
            }

            return levelData;
        }
    }

    public class Game
    {
        private bool _isRunning = true;
        private int _progress = 0;

        public async Task LoadLevel(string levelName)
        {
            LevelLoader levelLoader = new LevelLoader();

            Task<LevelData> loadTask = levelLoader.LoadLevelAsync(levelName);

            Task progressTask = UpdateProgressBar();

            Console.WriteLine($"Загрузка уровня \"{levelName}\"...");
            Console.WriteLine("Команды: /inventory, /help, /cancel");
            Console.WriteLine(new string('-', 50));

            while (!loadTask.IsCompleted && !loadTask.IsFaulted)
            {
                if (Console.KeyAvailable)
                {
                    string command = Console.ReadLine();
                    await ProcessCommand(command, loadTask);
                }

                await Task.Delay(100);
            }

            _isRunning = false;
            await progressTask;

            try
            {
                LevelData level = await loadTask;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nУровень \"{level.LevelName}\" загружен!");
                Console.ResetColor();

                Console.WriteLine($"Размер: {level.Width} x {level.Height}");
                Console.WriteLine($"Враги: {string.Join(", ", level.Enemies)}");
                Console.WriteLine("Ресурсы:");
                foreach (var resource in level.Resources)
                {
                    Console.WriteLine($"  - {resource.Key}: {resource.Value}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nОшибка: {ex.Message}");
                Console.ResetColor();
            }
        }

        private async Task UpdateProgressBar()
        {
            char[] animation = { '|', '/', '-', '\\' };
            int animIndex = 0;

            while (_isRunning)
            {
                Console.CursorLeft = 0;

                Console.Write("[");

                int filledBars = _progress / 5;
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(i < filledBars ? '#' : '-');
                }

                Console.Write($"] {_progress}% {animation[animIndex]}");

                animIndex = (animIndex + 1) % animation.Length;

                if (_progress < 100)
                {
                    _progress += 2;
                    if (_progress > 100) _progress = 100;
                }

                await Task.Delay(200);
            }

            Console.CursorLeft = 0;
            Console.WriteLine("[####################] 100%");
        }

        private async Task ProcessCommand(string command, Task<LevelData> loadTask)
        {
            switch (command.ToLower())
            {
                case "/inventory":
                    Console.WriteLine("\n[ИНВЕНТАРЬ]");
                    Console.WriteLine("  Зелья лечения: 5");
                    Console.WriteLine("  Золото: 250");
                    Console.WriteLine("  Ключ от подземелья: 1");
                    Console.WriteLine("  Факел: 1");
                    break;

                case "/help":
                    Console.WriteLine("\n[ДОСТУПНЫЕ КОМАНДЫ]");
                    Console.WriteLine("  /inventory - показать инвентарь");
                    Console.WriteLine("  /progress  - показать прогресс загрузки");
                    Console.WriteLine("  /status    - статус загрузки");
                    Console.WriteLine("  /cancel    - отменить загрузку");
                    Console.WriteLine("  /help      - показать эту справку");
                    break;

                case "/progress":
                    Console.WriteLine($"\nПрогресс загрузки: {_progress}%");
                    break;

                case "/status":
                    if (loadTask.IsCompleted)
                        Console.WriteLine("\nСтатус: загрузка завершена");
                    else if (loadTask.IsFaulted)
                        Console.WriteLine("\nСтатус: ошибка загрузки");
                    else
                        Console.WriteLine("\nСтатус: загружается...");
                    break;

                case "/cancel":
                    Console.WriteLine("\nОтмена загрузки... (эмулируем)");
                    break;

                default:
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        Console.WriteLine($"\nНеизвестная команда: {command}");
                    }
                    break;
            }
        }
    }
}