using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SaveSystem saveSystem = new SaveSystem();

                List<Item> starterItems = new List<Item>
                {
                    new Item("Лопата", ItemRarity.Common),
                    new Item("Кирка", ItemRarity.Common),
                    new Item("Меч", ItemRarity.Common),
                    new Item("Сливочное пиво", ItemRarity.Rare)
                };

                Player player = new Player("Bobby", 100, 50, 1200, starterItems);
                GameState gameState = new GameState(player, "Лес");

                GameState loadedGame;

                string saveDirectory = "Saves";

                bool isPlaying = true;
                while (isPlaying)
                {
                    Console.Clear();

                    Console.WriteLine("1 - Сохранить игру");
                    Console.WriteLine("2 - Загрузить игру");
                    Console.WriteLine("3 - Список сохранений");
                    Console.WriteLine("4 - Удалить сохранение");
                    Console.WriteLine("5 - Выход");
                    Console.Write("Введите значение: ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Введите имя сохранения: ");
                                string saveName = Console.ReadLine();
                                saveSystem.SaveGame(gameState, saveName);
                                Console.WriteLine("Сохранение выполнено успешно!");
                                break;
                            case 2:
                                Console.Write("Введите имя сохранения для загрузки: ");
                                string loadName = Console.ReadLine();
                                loadedGame = saveSystem.LoadGame(loadName);
                                gameState = loadedGame;
                                Console.WriteLine("Загрузка выполнена успешно!");
                                break;
                            case 3:
                                Console.WriteLine("\nСписок сохранений:");
                                foreach (string saveFile in saveSystem.GetAllSaveFiles(saveDirectory))
                                {
                                    Console.WriteLine(saveFile);
                                }
                                break;
                            case 4:
                                Console.Write("Введите имя файла: ");
                                saveSystem.DeleteSave(Console.ReadLine());
                                break;
                            case 5:
                                isPlaying = false;
                                break;
                        }
                    }

                    Console.WriteLine("\nНажмите для продолжения...");
                    Console.ReadKey(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public class Player
    {
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public int Level { get; private set; }
        [JsonInclude]
        public int Experience { get; private set; }
        [JsonInclude]
        public int Health { get; private set; }
        [JsonInclude]
        public int MaxHealth { get; private set; }
        [JsonInclude]
        public int Mana { get; private set; }
        [JsonInclude]
        public int MaxMana { get; private set; }
        [JsonInclude]
        public float Gold { get; private set; }
        [JsonInclude]
        public List<Item> Inventory { get; private set; } = new List<Item>();

        [JsonConstructor]
        public Player() { }

        public Player(string name, int maxHealth, int maxMana, float gold, List<Item> inventory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя игрока не может быть пустым");

            Name = name;

            if (maxHealth > 0)
            {
                Health = maxHealth;
                MaxHealth = maxHealth;
            }
            else throw new ArgumentException("Максимальное значение здоровья не может быть меньше одного");

            if (maxMana > 0)
            {
                Mana = maxMana;
                MaxMana = maxMana;
            }
            else throw new ArgumentException("Максимальное значение маны не может быть меньше одного");

            Gold = gold;

            Inventory = inventory;
            Level = 1;
            Experience = 0;
        }
    }

    public class Item
    {
        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public ItemRarity Rarity { get; private set; }

        private static int _nextId = 0;

        [JsonConstructor]
        public Item() { }

        public Item(string name, ItemRarity rarity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя предмета не может быть пустым");

            Id = _nextId++;
            Name = name;
            Rarity = rarity;
        }
    }

    public class QuestData
    {
        public string Id { get; set; }
        public bool Completed { get; set; }

        public QuestData() { }
        public QuestData(string id, bool completed)
        {
            Id = id;
            Completed = completed;
        }
    }

    public class GameState
    {
        [JsonInclude]
        public Player CurrentPlayer { get; private set; }
        [JsonInclude]
        public string CurrentLocation { get; private set; }
        [JsonInclude]
        public DateTime SaveTime { get; private set; }
        [JsonInclude]
        public int PlayTimeInMinutes { get; private set; }
        [JsonInclude]
        public List<string> UnlockedLocations { get; private set; } = new List<string>();
        [JsonIgnore]
        public Dictionary<string, bool> QuestProgress { get; private set; } = new Dictionary<string, bool>();

        public List<QuestData> SerializableQuests
        {
            get => QuestProgress.Select(kvp => new QuestData(kvp.Key, kvp.Value)).ToList();
            set => QuestProgress = value.ToDictionary(q => q.Id, q => q.Completed);
        }

        [JsonIgnore]
        private DateTime _startTime;

        [JsonConstructor]
        public GameState() { }

        public GameState(Player player, string location)
        {
            CurrentPlayer = player;
            CurrentLocation = location;

            _startTime = DateTime.Now;
        }

        public void UpdatePlayTime()
        {
            PlayTimeInMinutes = (int)(DateTime.Now - _startTime).TotalMinutes;
        }

        public void SetSaveTime()
        {
            SaveTime = DateTime.Now;
        }

        public DateTime GetStartTime() { return _startTime; }

        public void RestoreStartTime()
        {
            _startTime = SaveTime.AddMinutes(-PlayTimeInMinutes);
        }
    }

    public class SaveSystem
    {
        private JsonSerializerOptions _jsonOptions;

        public SaveSystem()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public void SaveGame(GameState gameState, string saveFileName)
        {
            gameState.UpdatePlayTime();
            gameState.SetSaveTime();

            string saveDirectory = "Saves";
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            string filePath = Path.Combine(saveDirectory, $"{saveFileName}.json");

            string json = JsonSerializer.Serialize(gameState, _jsonOptions);
            File.WriteAllText(filePath, json);

            Console.WriteLine("Данные сохранены в JSON");
        }

        public GameState LoadGame(string saveFileName)
        {
            string filePath = Path.Combine("Saves", $"{saveFileName}.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл сохранения не найден: {filePath}");

            string json = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<GameState>(json, _jsonOptions);

            if (gameState == null)
                throw new Exception("Ошибка загрузки данных");

            gameState.RestoreStartTime();
            gameState.UpdatePlayTime();

            Console.WriteLine("Данные загружены:");
            Console.WriteLine($"Игрок: {gameState.CurrentPlayer.Name}");
            Console.WriteLine($"Локация: {gameState.CurrentLocation}");
            Console.WriteLine($"Время сохранения: {gameState.SaveTime}");
            Console.WriteLine($"Время с первого входа в игру: {gameState.PlayTimeInMinutes} минут");
            Console.WriteLine($"Время начала игры: {gameState.GetStartTime()}");

            return gameState;
        }

        public List<string> GetAllSaveFiles(string saveDirectory)
        {
            if (!Directory.Exists(saveDirectory))
                return new List<string>();

            return Directory.GetFiles(saveDirectory, "*.json")
                            .Select(filePath => new FileInfo(filePath))
                            .Select(fileInfo => $"{fileInfo.Name} ({fileInfo.LastWriteTime:yyyy-MM-dd HH:mm})")
                            .ToList();
        }

        public void DeleteSave(string saveFileName)
        {
            if (string.IsNullOrWhiteSpace(saveFileName))
            {
                Console.WriteLine("Имя файла не может быть пустым");
                return;
            }

            if (!saveFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                saveFileName += ".json";

            string filePath = Path.Combine("Saves", saveFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Сохранение {saveFileName} удалено");
            }
            else
                Console.WriteLine($"Файл {saveFileName} не найден");
        }
    }
}
