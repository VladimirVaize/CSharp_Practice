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
            Game.GameProcess();
        }
    }

    public class Game
    {
        static Random random = new Random();
        public static void GameProcess()
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

                bool isPlaying = true;
                
                while (isPlaying)
                {
                    Console.Clear();
                    Console.WriteLine($"=== ИГРА: {gameState.CurrentPlayer.Name} ===");
                    Console.WriteLine($"Локация: {gameState.CurrentLocation}");
                    Console.WriteLine($"Уровень: {gameState.CurrentPlayer.Level} | Опыт: {gameState.CurrentPlayer.Experience}/100");
                    Console.WriteLine($"HP: {gameState.CurrentPlayer.Health}/{gameState.CurrentPlayer.MaxHealth}");
                    Console.WriteLine($"Mana: {gameState.CurrentPlayer.Mana}/{gameState.CurrentPlayer.MaxMana}");
                    Console.WriteLine($"Золото: {gameState.CurrentPlayer.Gold}");
                    Console.WriteLine($"Инвентарь: {gameState.CurrentPlayer.Inventory.Count} предметов");
                    Console.WriteLine(new string('-', 40));

                    Console.WriteLine("1 - Идти в следующую локацию");
                    Console.WriteLine("2 - Найти предмет");
                    Console.WriteLine("3 - Получить опыт (+10)");
                    Console.WriteLine("4 - Сохранить игру");
                    Console.WriteLine("5 - Загрузить игру");
                    Console.WriteLine("6 - Показать статус (подробно)");
                    Console.WriteLine("7 - Список сохранений");
                    Console.WriteLine("8 - Выйти");
                    Console.Write("Выберите действие: ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                ChangeLocation(gameState);
                                break;
                            case 2:
                                FindRandomItem(gameState, random);
                                break;
                            case 3:
                                GainExperience(gameState, 10);
                                break;
                            case 4:
                                Console.Write("Имя сохранения: ");
                                string saveName = Console.ReadLine();
                                saveSystem.SaveGame(gameState, saveName);
                                Console.WriteLine("Игра сохранена!");
                                break;
                            case 5:
                                Console.Write("Имя сохранения: ");
                                string loadName = Console.ReadLine();
                                try
                                {
                                    gameState = saveSystem.LoadGame(loadName);
                                    Console.WriteLine("Игра загружена!");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                                }
                                break;
                            case 6:
                                ShowDetailedStatus(gameState);
                                break;
                            case 7:
                                ShowSaveList(saveSystem);
                                break;
                            case 8:
                                isPlaying = false;
                                continue;
                        }
                    }

                    Console.WriteLine("\nНажмите любую клавишу...");
                    Console.ReadKey(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ChangeLocation(GameState gameState)
        {
            string[] locations = { "Лес", "Горы", "Деревня", "Подземелье", "Замок" };

            string newLocation;
            do
            {
                newLocation = locations[new Random().Next(locations.Length)];
            } while (newLocation == gameState.CurrentLocation);

            gameState.SetCurrentLocation(newLocation);

            if (!gameState.UnlockedLocations.Contains(newLocation))
            {
                gameState.UnlockedLocations.Add(newLocation);
                Console.WriteLine($"Вы открыли новую локацию: {newLocation}!");

                string questId = "explorer";
                if (gameState.UnlockedLocations.Count >= 3 &&
                    gameState.QuestProgress.ContainsKey(questId) &&
                    !gameState.QuestProgress[questId])
                {
                    gameState.QuestProgress[questId] = true;
                    Console.WriteLine("Квест 'Исследователь' выполнен! +50 опыта");
                    GainExperience(gameState, 50);
                }
            }

            Console.WriteLine($"Вы переместились в {newLocation}");
        }

        static void FindRandomItem(GameState gameState, Random random)
        {
            string[] itemNames = {
                "Зелье здоровья", "Зелье маны", "Золотая монета", "Алмаз",
                "Руда", "Трава", "Гриб", "Старый свиток"
            };

            ItemRarity[] rarities = {
                ItemRarity.Common, ItemRarity.Common, ItemRarity.Common,
                ItemRarity.Rare, ItemRarity.Rare, ItemRarity.Epic
            };

            string name = itemNames[random.Next(itemNames.Length)];
            ItemRarity rarity = rarities[random.Next(rarities.Length)];

            Item newItem = new Item(name, rarity);
            gameState.CurrentPlayer.Inventory.Add(newItem);

            Console.WriteLine($"Вы нашли: {name} ({rarity})!");

            if (name == "Старый свиток" && gameState.QuestProgress.ContainsKey("find_scroll") && !gameState.QuestProgress["find_scroll"])
            {
                gameState.QuestProgress["find_scroll"] = true;
                Console.WriteLine("Задание 'Найти древний свиток' выполнено!");
            }

            if (name == "Золотая монета" || name == "Алмаз")
            {
                gameState.CurrentPlayer.AddGold(name == "Золотая монета" ? 10 : 50);

                if (gameState.CurrentPlayer.Gold >= 500 &&
                    gameState.QuestProgress.ContainsKey("rich") &&
                    !gameState.QuestProgress["rich"])
                {
                    gameState.QuestProgress["rich"] = true;
                    Console.WriteLine("Квест 'Богач' выполнен! +100 опыта");
                    GainExperience(gameState, 100);
                }
            }
        }

        static void GainExperience(GameState gameState, int amount)
        {
            gameState.CurrentPlayer.AddExperience(amount);
            Console.WriteLine($"Получено {amount} опыта!");

            while (gameState.CurrentPlayer.Experience >= 100)
            {
                gameState.CurrentPlayer.LevelUp();

                Console.WriteLine($"ПОВЫШЕНИЕ УРОВНЯ! Теперь уровень {gameState.CurrentPlayer.Level}");
                Console.WriteLine($"Характеристики увеличены!");
            }
        }

        static void ShowDetailedStatus(GameState gameState)
        {
            Console.WriteLine("\n=== ПОДРОБНЫЙ СТАТУС ===");
            Console.WriteLine($"Игрок: {gameState.CurrentPlayer.Name}");
            Console.WriteLine($"Уровень: {gameState.CurrentPlayer.Level}");
            Console.WriteLine($"Опыт: {gameState.CurrentPlayer.Experience}/100");
            Console.WriteLine($"Здоровье: {gameState.CurrentPlayer.Health}/{gameState.CurrentPlayer.MaxHealth}");
            Console.WriteLine($"Мана: {gameState.CurrentPlayer.Mana}/{gameState.CurrentPlayer.MaxMana}");
            Console.WriteLine($"Золото: {gameState.CurrentPlayer.Gold}");
            Console.WriteLine($"Локация: {gameState.CurrentLocation}");

            Console.WriteLine("\nОткрытые локации:");
            foreach (var loc in gameState.UnlockedLocations)
                Console.WriteLine($"  - {loc}");

            Console.WriteLine("\nИнвентарь:");
            if (gameState.CurrentPlayer.Inventory.Count == 0)
                Console.WriteLine("  Пусто");
            else
                foreach (var item in gameState.CurrentPlayer.Inventory)
                    Console.WriteLine($"  - {item.Name} ({item.Rarity})");

            Console.WriteLine("\nПрогресс квестов:");
            if (gameState.QuestProgress.Count == 0)
                Console.WriteLine("  Нет активных квестов");
            else
                foreach (var quest in gameState.QuestProgress)
                    Console.WriteLine($"  - {quest.Key}: {(quest.Value ? "(Выполнен)" : "(В процессе)")}");
        }

        static void ShowSaveList(SaveSystem saveSystem)
        {
            var saves = saveSystem.GetAllSaveFiles("Saves");
            Console.WriteLine("\nСохранения:");
            if (saves.Count == 0)
                Console.WriteLine("  Нет сохранений");
            else
                foreach (var save in saves)
                    Console.WriteLine($"  - {save}");
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

        public void LevelUp()
        {
            Level++;
            Experience -= 100;
            MaxHealth += 20;
            Health = MaxHealth;
            MaxMana += 10;
            Mana = MaxMana;
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Нельзя добавить отрицательное значение опыта");

            Experience += amount;
        }

        public void AddGold(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Нульзя добавить отрицательное значение золота");

            Gold += amount;
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
            UnlockedLocations = new List<string> { location };
            QuestProgress = new Dictionary<string, bool>
            {
                { "explorer", false },
                { "find_scroll", false },
                { "rich", false }
            };

            _startTime = DateTime.Now;
        }

        public void SetCurrentLocation(string newLocation)
        {
            if(newLocation == null) 
            {
                throw new ArgumentNullException("Локация не может быть null");
            }

            CurrentLocation = newLocation;
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
